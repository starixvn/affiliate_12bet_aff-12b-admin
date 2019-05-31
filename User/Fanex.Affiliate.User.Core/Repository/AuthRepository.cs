using System;
using System.Data;
using Fanex.Affiliate.User.Core.Constants;
using Fanex.Affiliate.User.Core.DTO;
using Fanex.Affiliate.User.Core.DTO.Auth;
using Fanex.Affiliate.User.Core.MetalModels;
using Fanex.Data;

namespace Fanex.Affiliate.User.Core.Repository
{
    public class AuthRepository : IAuthRepository
    {
        public int GetOnUserId(OnUserIdParam onUserIdParam)
        {
            const string spKey = "Aff_CheckIn";
            var dbMode = onUserIdParam.IsInternal ? DBMode.View : DBMode.Main;

            using (var objectDb = new ObjectDBBuilder(spKey, dbMode))
            {
                objectDb.DB.ExecuteNonQuery(onUserIdParam);
            }

            return onUserIdParam.On_UserId;
        }

        public AuthorizedUserInfo GetUserInfo(string userName, bool isInternal)
        {
            const string spKey = "Aff_User_Login_UserInfoSel";
            var dbMode = isInternal ? DBMode.View : DBMode.Main;

            using (var objectDbBuilder = new ObjectDBBuilder(spKey, dbMode))
            {
                objectDbBuilder.DB.AddInParameter("UserName", SqlDbType.NVarChar, userName);

                using (var reader = objectDbBuilder.DB.ExecuteReader())
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

            using (var objectDbBuilder = new ObjectDBBuilder(spKey))
            {
                var objectDb = objectDbBuilder.DB;
                AssignUpdateLoginResultParam(objectDb, loginResultParam);
                objectDb.ExecuteNonQuery();

                var code = objectDb.GetParameterValue("ErrCode");

                return new LoginResultInfo
                {
                    IsOk = code == null || Convert.ToInt32(code) == 0,
                    Message = Convert.ToString(objectDb.GetParameterValue("ErrMsg"))
                };
            }
        }

        public string GetSessionId(int onUserId)
        {
            const string spKey = "Aff_SessionIdSel";

            using (var objectDb = new ObjectDBBuilder(spKey))
            {
                objectDb.DB.AddInParameter("OnUserId", System.Data.SqlDbType.Int, onUserId);

                return objectDb.DB.ExecuteScalar() as string;
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

            using (var objectDb = new ObjectDBBuilder(spKey, dbMode))
            {
                objectDb.DB.ExecuteNonQuery(parameterSet);
            }

            return parameterSet.IsKickedOut;
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