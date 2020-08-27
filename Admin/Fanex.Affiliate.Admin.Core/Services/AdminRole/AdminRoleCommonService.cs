using Fanex.Affiliate.Admin.Core.DTO.AdminRole;
using Fanex.Affiliate.Admin.Core.Repository;
using Fanex.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fanex.Affiliate.Admin.Core.Services.AdminRole
{
    public class AdminRoleCommonService : BaseRepository, IAdminRoleCommonService
    {
        public IEnumerable<MarketInfo> GetMarketList(int? userId = null)
        {
            const string SpKey = "Aff_Admin_AdminUser_RoleMarket_Sel";
            var result = new List<MarketInfo>();

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.AddInParameter("RoleId", DbType.String, userId);

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
    }
}
