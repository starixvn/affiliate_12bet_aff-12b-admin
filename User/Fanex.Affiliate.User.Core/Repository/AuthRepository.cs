using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Fanex.Affiliate.User.Core.Constants;
using Fanex.Affiliate.User.Core.DTO;
using Fanex.Affiliate.User.Core.DTO.Auth;
using Fanex.Affiliate.User.Core.MetalModels;
using Fanex.Data;
using Fanex.Common.Extensions;

namespace Fanex.Affiliate.User.Core.Repository
{
    public class AuthRepository : IAuthRepository
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
                db.AddInParameter("UserName", SqlDbType.NVarChar, userName);

                using (var reader = db.ExecuteReader())
                {
                    return reader.Read() ? new AuthorizedUserInfo
                    {
                        AffId = Convert.ToInt32(reader["AffId"]),
                        UserName = Convert.ToString(reader["UserName"]),
                        Password = Convert.ToString(reader["Password"]),
                        GroupName = Convert.ToString(reader["GroupName"]),
                        AffType = Convert.ToString(reader["AffType"]),
                        IsClosed = Convert.ToBoolean(reader["IsClosed"]),
                        Status = Convert.ToString(reader["Status"]),
                        PreferredLanguage = Convert.ToString(reader["PreferredLanguage"])
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

                var code = db.GetParameterValue("ErrCode");

                return new LoginResultInfo
                {
                    IsOk = code == null || Convert.ToInt32(code) == 0,
                    Message = Convert.ToString(db.GetParameterValue("ErrMsg"))
                };
            }
        }

        public InternalLoginInfo GetInternalLoginInfo(int onUserId)
        {
            const string spKey = "Aff_SessionIdSel";
            using (var db = _builder.Build(spKey))
            {
                using (var reader = db.ExecuteReader(new { OnUserId = onUserId }))
                {
                    reader.Read();
                    return new InternalLoginInfo
                    {
                        SesionId = reader.ToString("NetSessionID"),
                        UserId = reader.ToInt("CustId")
                    };
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
                using(var reader = db.ExecuteReader(new { UserId = internalLoginUserId }))
                {
                    while(reader.Read())
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
            objectDb.AddInParameter("AffId", SqlDbType.Int, param.AffId);
            objectDb.AddInParameter("IpAddress", SqlDbType.NVarChar, param.IpAddress);
            objectDb.AddInParameter("DeviceInfo", SqlDbType.NVarChar, param.DeviceInfo);
            objectDb.AddInParameter("Language", SqlDbType.VarChar, param.Language);
            objectDb.AddInParameter("IsSuccessful", SqlDbType.Bit, param.IsSuccessful);

            objectDb.AddOutParameter("ErrCode", SqlDbType.Int, DbParam.ErrCodeSize);
            objectDb.AddOutParameter("ErrMsg", SqlDbType.NVarChar, DbParam.ErrMsgSize);
        }
    }
}