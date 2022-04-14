using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.AdminRole;
using Fanex.Affiliate.Admin.Core.MetaModels.AdminRole;
using Fanex.Common.Extensions;
using System.Collections.Generic;
using System.Data;

namespace Fanex.Affiliate.Admin.Core.Repository.AdminRole
{
    public class RolePermissionRepository : BaseRepository, IRolePermissionRepository
    {
        public IEnumerable<RolePermissionInfo> GetPermissionInfoList()
        {
            const string SpKey = "Aff_Admin_AdminUser_RolePermission_Sel";
            var result = new List<RolePermissionInfo>();

            using (var objectDb = GetObjectDb(SpKey))
            {
                using (var reader = objectDb.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new RolePermissionInfo
                        {
                            RoleId = reader.ToInt("RoleId"),
                            RoleName = reader.ToString("RoleName"),
                            DisplayName = reader.ToString("DisplayName"),
                            ParentId = reader.ToInt("ParentId"),
                            AllParentIds = reader.ToString("AllParentIds"),
                            FuncId = reader.ToInt("FuncId"),
                            FuncName = reader.ToString("FuncName"),
                            Policy = reader.ToInt("Policy")
                        });
                    }
                }
            }

            return result;
        }

        public ResultInfo UpdatePermissionInfo(UpdateRolePermissionParam param)
        {
            const string SpKey = "Aff_Admin_AdminUser_RolePermission_Upd";

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.SetParameterValue("Permission", param.PermissionInfoXml);
                objectDb.SetParameterValue("UserId", param.UserId);
                objectDb.SetParameterValue("IP", param.Ip);

                AssignResultOutputParam(objectDb);

                using (var reader = objectDb.ExecuteReader())
                {
                    return GetResultInfo(objectDb);
                }
            }
        }

        public ResultInfo UpdateMarketInfoList(UpdateMarketInfoParam param)
        {
            const string SpKey = "Aff_Admin_AdminUser_RoleMarket_Upd";

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.SetParameterValue("Market", param.MarketInfoXml);
                objectDb.SetParameterValue("UserId", param.UserId);
                objectDb.SetParameterValue("IP", param.Ip);

                AssignResultOutputParam(objectDb);

                using (var reader = objectDb.ExecuteReader())
                {
                    return GetResultInfo(objectDb);
                }
            }
        }
    }
}
