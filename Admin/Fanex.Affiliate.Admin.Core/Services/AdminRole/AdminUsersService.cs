using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.AdminRole;
using Fanex.Affiliate.Admin.Core.MetaModels.AdminRole;
using Fanex.Affiliate.Admin.Core.Repository;
using Fanex.Common.Extensions;
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
                UserId = reader.ToInt("CustId"),
                UserName = reader.ToString("UserName"),
                FirstName = reader.ToString("FirstName"),
                LastName = reader.ToString("LastName"),
                CreatedDate = reader.ToDateTime("CreatedDate"),
                LastLoginDate = reader.ToDateTime("LastLoginDate"),
                LastLoginIp = reader.ToString("LastLoginIp"),
                RoleName = reader.ToString("RoleName"),
                IsActive = !reader.ToBool("Closed")
            };
        }
    }
}
