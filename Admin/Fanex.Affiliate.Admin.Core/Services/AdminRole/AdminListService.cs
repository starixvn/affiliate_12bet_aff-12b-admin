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
    public class AdminListService : BaseRepository, IAdminListService
    {
        public ResultInfo DeleteUsers(DeleteAdminUsersParam param)
        {
            const string SpKey = "Aff_Admin_AdminUser_Del";

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.AddInParameter("CustIds", DbType.String, param.UserIds);
                objectDb.AddInParameter("UserId", DbType.Int32, param.ByUserId);
                objectDb.AddInParameter("Ip", DbType.String, param.Ip);

                AssignResultOutputParam(objectDb);

                objectDb.ExecuteNonQuery();

                return GetResultInfo(objectDb);
            }
        }

        public IEnumerable<AdminListInfo> GetUserList(string userName)
        {
            const string SpKey = "Aff_Admin_AdminUser_Sel";
            var result = new List<AdminListInfo>();

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

        private AdminListInfo BuildItem(IDataReader reader)
        {
            return new AdminListInfo
            {
                UserId = reader.ToInt("CustId"),
                UserName = reader.ToString("UserName"),
                FirstName = reader.ToString("FirstName"),
                LastName = reader.ToString("LastName"),
                Creator = reader.ToString("Creator"),
                LastLoginDate = reader["LastLoginDate"] == DBNull.Value ? (DateTime?)null : reader.ToDateTime("LastLoginDate"),
                LastLoginIp = reader.ToString("LastLoginIp"),
                RoleName = reader.ToString("RoleName"),
                IsActive = !reader.ToBool("Closed")
            };
        }
    }
}
