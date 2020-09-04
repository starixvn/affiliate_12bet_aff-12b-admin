using Fanex.Affiliate.Admin.Core.DTO.AdminRole;
using System.Collections.Generic;

namespace Fanex.Affiliate.Admin.Core.Services.AdminRole
{
    public interface IGeneralPermissionRoleService
    {
        IEnumerable<PermissionInfo> GetPermissionInfoList();

        IEnumerable<MarketInfo> GetMarketByRoleList(int roleId);
    }
}
