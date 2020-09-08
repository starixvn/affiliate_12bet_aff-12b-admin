using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.AdminRole;
using Fanex.Affiliate.Admin.Core.MetaModels.AdminRole;
using Fanex.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Data;

namespace Fanex.Affiliate.Admin.Core.Repository.AdminRole
{
    public class GeneralPermissionRoleRepository : BaseRepository, IGeneralPermissionRoleRepository
    {
        public IEnumerable<MarketInfo> GetMarketByRoleList()
        {
            const string SpKey = "Aff_Admin_AdminUser_RoleMarket_Sel";
            var result = new List<MarketInfo>();

            using (var objectDb = GetObjectDb(SpKey))
            {
                using (var reader = objectDb.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new MarketInfo
                        {
                            RoleId = reader.ToInt("RoleId"),
                            MarketId = reader.ToString("MarketId"),
                            MarketName = reader.ToString("DisplayName"),
                            HasPermission = Convert.ToBoolean(reader["Status"])
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

        public ResultInfo UpdatePermissionInfo(UpdatePermissionInfoParam param)
        {
            const string SpKey = "Aff_Admin_AdminUser_RolePermission_Upd";

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.AddInParameter("RoleId", DbType.Int32, param.RoleId);
                objectDb.AddInParameter("Permission", DbType.Xml, param.PermissionInfoXml);
                objectDb.AddInParameter("UserId", DbType.Int32, param.UserId);
                objectDb.AddInParameter("IP", DbType.String, param.Ip);

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
                objectDb.AddInParameter("Market", DbType.Xml, param.MarketInfoXml);
                objectDb.AddInParameter("UserId", DbType.Int32, param.UserId);
                objectDb.AddInParameter("IP", DbType.String, param.Ip);

                AssignResultOutputParam(objectDb);

                using (var reader = objectDb.ExecuteReader())
                {
                    return GetResultInfo(objectDb);
                }
            }
        }
    }
}
