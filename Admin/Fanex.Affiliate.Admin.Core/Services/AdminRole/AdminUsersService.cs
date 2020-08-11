using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.AdminRole;
using Fanex.Affiliate.Admin.Core.MetaModels.AdminRole;
using Fanex.Affiliate.Admin.Core.Repository;
using System;
using System.Collections.Generic;
using System.Data;

namespace Fanex.Affiliate.Admin.Core.Services.AdminRole
{
    public class AdminUsersService : BaseRepository, IAdminUsersService
    {
        public ResultInfo DeleteUsers(DeleteAdminUsersParam param)
        {
            const string SpKey = "";

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.AddInParameter("UserIds", DbType.String, param.UserIds);
                objectDb.AddInParameter("UserId", DbType.Int32, param.ByUserId);
                objectDb.AddInParameter("Ip", DbType.String, param.Ip);

                AssignResultOutputParam(objectDb);

                objectDb.ExecuteNonQuery();

                return GetResultInfo(objectDb);
            }
        }

        public IEnumerable<AdminUserInfo> GetUserList(string userName)
        {
            const string SpKey = "Aff_Admin_AdminUser_Sel";
            var result = new List<AdminUserInfo>();

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.AddInParameter("UserName", DbType.String, userName);

                using (var reader = objectDb.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(BuildItem(reader));
                    }
                }
            }

            return result;
        }

        private AdminUserInfo BuildItem(IDataReader reader)
        {
            return new AdminUserInfo
            {
                UserId = Convert.ToInt32(reader["CustId"]),
                UserName = Convert.ToString(reader["UserName"]),
                CreatedDate = Convert.ToDateTime(reader["CreatedDate"]),
                LastLoginDate = reader["LastLoginDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["LastLoginDate"]),
                LastLoginIp = Convert.ToString(reader["LastLoginIp"]),
                RoleName = Convert.ToString(reader["RoleName"])
            };
        }
    }
}
