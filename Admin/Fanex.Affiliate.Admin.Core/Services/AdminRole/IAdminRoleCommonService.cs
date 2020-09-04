
using Fanex.Affiliate.Admin.Core.DTO.AdminRole;
using System.Collections.Generic;

namespace Fanex.Affiliate.Admin.Core.Services.AdminRole
{
    public interface IAdminRoleCommonService
    {
        IEnumerable<RoleInfo> GetRoleList();

        IEnumerable<MarketInfo> GetMarketList(int userId);
    }
}
