using Fanex.Affiliate.Admin.Core.DTO.AdminRole;
using Fanex.Affiliate.Admin.Core.Repository;
using Fanex.Common.Extensions;
using System.Collections.Generic;
using System.Data;

namespace Fanex.Affiliate.Admin.Core.Services.AdminRole
{
    public class GeneralPermissionRoleService : BaseRepository, IGeneralPermissionRoleService
    {
        public IEnumerable<MarketInfo> GetMarketByRoleList(int roleId)
        {
            const string SpKey = "Aff_Admin_AdminUser_RoleMarket_Sel";
            var result = new List<MarketInfo>();

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.AddInParameter("RoleId", DbType.String, roleId);

                using (var reader = objectDb.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new MarketInfo
                        {
                            RoleId = reader.ToInt("RoleId"),
                            MarketId = reader.ToString("MarketId"),
                            MarketName = reader.ToString("DisplayName"),
                            HasPermission = reader.ToBool("Status")
                        });
                    }
                }
            }

            return result;
        }

        public IEnumerable<PermissionInfo> GetPermissionInfoList()
        {
            const string SpKey = "Aff_Admin_AdminUser_RolePermission_Sel";
            var result = new List<PermissionInfo>();

            using (var objectDb = GetObjectDb(SpKey))
            {
                using (var reader = objectDb.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new PermissionInfo
                        {
                            RoleId = reader.ToInt("RoleId"),
                            RoleName = reader.ToString("RoleName"),
                            ParentId = reader.ToInt("ParentId"),
                            FuncId = reader.ToInt("FuncId"),
                            FuncName = reader.ToString("FuncName"),
                            Policy = reader.ToInt("Policy")
                        });
                    }
                }
            }

            return result;
        }
    }
}
