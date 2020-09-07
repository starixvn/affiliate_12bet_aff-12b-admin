using Fanex.Affiliate.Admin.Core.DTO.AdminRole;
using Fanex.Affiliate.Admin.Core.Repository;
using Fanex.Common.Extensions;
using System.Collections.Generic;
using System.Data;

namespace Fanex.Affiliate.Admin.Core.Services.AdminRole
{
    public class AdminRoleCommonService : BaseRepository, IAdminRoleCommonService
    {
        public IEnumerable<RoleInfo> GetRoleList()
        {
            const string SpKey = "Aff_Admin_AdminUser_Role_Sel";
            var result = new List<RoleInfo>();

            using (var objectDb = GetObjectDb(SpKey))
            {
                using (var reader = objectDb.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new RoleInfo
                        {
                            RoleId = reader.ToInt("RoleId"),
                            RoleName = reader.ToString("RoleName")
                        });
                    }
                }
            }

            return result;
        }

        public IEnumerable<MarketInfo> GetMarketByUserList(int userId)
        {
            const string SpKey = "Aff_Admin_AdminUser_Market_Sel";
            var result = new List<MarketInfo>();

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.AddInParameter("CustId", DbType.String, userId);

                using (var reader = objectDb.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new MarketInfo
                        {
                            MarketId = reader.ToString("MarketId"),
                            MarketName = reader.ToString("MarketName"),
                            HasPermission = reader.ToBool("Status")
                        });
                    }
                }
            }

            return result;
        }
    }
}
