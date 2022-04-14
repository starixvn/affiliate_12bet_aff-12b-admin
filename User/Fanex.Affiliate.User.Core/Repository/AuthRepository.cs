using Fanex.Affiliate.User.Core.Constants;
using Fanex.Affiliate.User.Core.DTO;
using Fanex.Affiliate.User.Core.DTO.Auth;
using Fanex.Affiliate.User.Core.MetalModels;
using Fanex.Common.Extensions;
using Fanex.Data;
using System;
using System.Collections.Generic;

namespace Fanex.Affiliate.User.Core.Repository
{
    public class AuthRepository : BaseRepository, IAuthRepository
    {
        private readonly IObjectDBBuilder _builder;

        public AuthRepository(IObjectDBBuilder builder)
        {
            _builder = builder;
        }

        public int GetOnUserId(OnUserIdParam onUserIdParam)
        {
            const string spKey = "Aff_CheckIn";
            var dbMode = onUserIdParam.IsInternal ? DBMode.View : DBMode.Main;

            using (var db = _builder.Build(spKey, dbMode))
            {
                db.ExecuteNonQuery(onUserIdParam);
            }

            return onUserIdParam.On_UserId;
        }

        public AuthorizedUserInfo GetUserInfo(string userName, bool isInternal)
        {
            const string spKey = "Aff_User_Login_UserInfoSel";
            var dbMode = isInternal ? DBMode.View : DBMode.Main;

            using (var db = _builder.Build(spKey, dbMode))
            {
                db.SetParameterValue("UserName", userName);

                using (var reader = db.ExecuteReader())
                {
                    return reader.Read() ? new AuthorizedUserInfo
                    {
                        AffId = reader.ToInt("AffId"),
                        UserName = reader.ToString("UserName"),
                        Password = reader.ToString("Password"),
                        GroupName = reader.ToString("GroupName"),
                        AffType = reader.ToString("AffType"),
                        IsClosed = reader.ToBool("IsClosed"),
                        Status = reader.ToString("Status"),
                        PreferredLanguage = reader.ToString("PreferredLanguage"),
                        OfferPageId = reader.ToInt("OfferPageId")
                    } : null;
                }
            }
        }

        public LoginResultInfo UpdateLoginResult(LoginResultParam loginResultParam)
        {
            const string spKey = "Aff_User_Login_ResultUpd";

            using (var db = _builder.Build(spKey))
            {
                AssignUpdateLoginResultParam(db, loginResultParam);
                db.ExecuteNonQuery();

                var result = GetResultInfo(db);

                return new LoginResultInfo
                {
                    IsOk = result.IsOk,
                    Message = result.Message
                };
            }
        }

        public InternalLoginInfo GetInternalLoginInfo(int onUserId)
        {
            const string spKey = "Aff_SessionIdSel";
            using (var db = _builder.Build(spKey))
            {
                db.SetParameterValue("OnUserId", onUserId);

                using (var reader = db.ExecuteReader())
                {
                    return reader.Read() ? new InternalLoginInfo
                    {
                        SesionId = reader.ToString("NetSessionID"),
                        UserId = reader.ToInt("CustId")
                    } : null;
                }
            }
        }

        public bool Recheckin(int onUserId, bool isInternal)
        {
            const string spKey = "Aff_Recheckin";
            var dbMode = isInternal ? DBMode.View : DBMode.Main;
            var parameterSet = new
            {
                On_UserId = onUserId,
                IsKickedOut = true
            };

            using (var db = _builder.Build(spKey, dbMode))
            {
                db.ExecuteNonQuery(parameterSet);
            }

            return parameterSet.IsKickedOut;
        }

        public IEnumerable<InternalPermission> GetInternalLoginPermission(int internalLoginUserId)
        {
            const string spKey = "Aff_Admin_PolicySel";
            var result = new List<InternalPermission>();
            using (var db = _builder.Build(spKey))
            {
                db.SetParameterValue("UserId", internalLoginUserId);

                using (var reader = db.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new InternalPermission
                        {
                            FunctionName = reader.ToString("FuncName"),
                            IsHost = reader.ToByte("Host") == 1
                        });
                    }
                }
            }
            return result;
        }

        private static void AssignUpdateLoginResultParam(IObjectDb objectDb, LoginResultParam param)
        {
            objectDb.SetParameterValue("AffId", param.AffId);
            objectDb.SetParameterValue("IpAddress", param.IpAddress);
            objectDb.SetParameterValue("DeviceInfo", param.DeviceInfo);
            objectDb.SetParameterValue("Language", param.Language);
            objectDb.SetParameterValue("IsSuccessful", param.IsSuccessful);

            AssignResultOutputParam(objectDb);
        }
    }
}