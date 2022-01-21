using Fanex.Affiliate.Admin.Core.DTO.AdminRole;
using Fanex.Affiliate.Admin.Core.Repository;
using Fanex.Common.Extensions;
using System;
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

        public IEnumerable<MarketInfo> GetMarketListByUser(int? userId = null)
        {
            const string SpKey = "Aff_Admin_AdminUser_Market_Sel";
            var result = new List<MarketInfo>();

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.SetParameterValue("CustId", userId);

                using (var reader = objectDb.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(BuildMarketItem(reader));
                    }
                }
            }

            return result;
        }

        public IEnumerable<MarketInfo> GetMarketListByRole(int? roleId = null)
        {
            const string SpKey = "Aff_Admin_AdminUser_RoleMarket_Sel";
            var result = new List<MarketInfo>();

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.SetParameterValue("RoleId", roleId);

                using (var reader = objectDb.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var item = BuildMarketItem(reader);
                        item.RoleId = reader.ToInt("RoleId");

                        result.Add(item);
                    }
                }
            }

            return result;
        }

        private static MarketInfo BuildMarketItem(IDataReader reader)
        {
            return new MarketInfo
            {
                MarketId = reader.ToString("MarketId"),
                MarketName = reader.ToString("MarketName"),
                HasPermission = Convert.ToBoolean(reader["Status"])
            };
        }
    }
}
