using System;
using System.Collections.Generic;
using System.Data;
using Fanex.Affiliate.Admin.Core.DTO.Common;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.MetaModels.Login;
using DNAide.Utils;

namespace Fanex.Affiliate.Admin.Core.Repository
{
    public class AuthRepository : BaseRepository, IAuthRepository
    {
        public IList<UserPermission> GetPermissionList(int userId)
        {
            const string SpKey = "Aff_Admin_PolicySel";
            var result = new List<UserPermission>();

            using (var objectDb = GetObjectDb(SpKey))
            {
                using (var reader = objectDb.ExecuteReader(new { userId }))
                {
                    while (reader.Read())
                    {
                        result.Add(new UserPermission
                        {
                            FunctionName = reader["FuncName"].ToString(),
                            IsHost = Convert.ToBoolean(reader["Host"])
                        });
                    }
                }
            }

            return result;
        }

        public bool ReCheckin(int onUserId)
        {
            const string SpKey = "Aff_Recheckin";

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.AddInParameter(Field.OnUserId, DbType.Int32, onUserId);
                objectDb.AddOutParameter("iskickedout", DbType.Int32, Size.SizeOfInt);
                objectDb.ExecuteNonQuery();
                var isKickedOut = objectDb.GetParameterValue("iskickedout");

                return (isKickedOut != DBNull.Value) && Convert.ToBoolean(isKickedOut);
            }
        }

        public ResultInfo Login(LoginParam param)
        {
            const string SpKey = "Aff_Admin_Login";

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.AddInParameter("UserName", DbType.String, param.UserName);
                objectDb.AddInParameter("UserPwd", DbType.String, Encryption.CfsEnCode(param.Password));
                objectDb.AddInParameter("IP", DbType.String, param.Ip);
                objectDb.AddInParameter("HostName", DbType.String, param.UserHostName);

                objectDb.AddOutParameter("UserId", DbType.Int32, Size.SizeOfInt);

                AssignResultOutputParam(objectDb);

                objectDb.ExecuteNonQuery();

                var userId = objectDb.GetParameterValue("UserId");

                param.UserId = userId == DBNull.Value ? 0 : Convert.ToInt32(userId);

                return GetResultInfo(objectDb);
            }
        }

        public int LoginOnUser(LoginParam param, string sessionId, string serverId)
        {
            const int AffiliateAdminSite = 6;
            const string SpKey = "Aff_CheckIn";

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.AddInParameter("custid", DbType.Int32, param.UserId);
                objectDb.AddInParameter("netsessionid", DbType.String, sessionId);
                objectDb.AddInParameter("serverid", DbType.String, serverId);
                objectDb.AddInParameter("ip", DbType.String, param.Ip);
                objectDb.AddInParameter("subaccid", DbType.Int32, AffiliateAdminSite);

                objectDb.AddOutParameter(Field.OnUserId, DbType.Int32, Size.SizeOfInt);

                objectDb.ExecuteNonQuery();

                var userId = objectDb.GetParameterValue(Field.OnUserId);

                return userId == DBNull.Value ? 0 : Convert.ToInt32(userId);
            }
        }

        public void Logout(int onUserId)
        {
            const string SpKey = "Aff_Logout";

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.AddInParameter(Field.OnUserId, DbType.Int32, onUserId);

                objectDb.ExecuteNonQuery();
            }
        }
    }
}