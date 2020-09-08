using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.AdminRole;
using Fanex.Affiliate.Admin.Core.MetaModels.AdminRole;
using System.Collections.Generic;

namespace Fanex.Affiliate.Admin.Core.Services.AdminRole
{
    public interface IGeneralPermissionRoleService
    {
        IEnumerable<PermissionInfo> GetPermissionInfoList();

        IEnumerable<MarketInfo> GetMarketByRoleList();

        ResultInfo UpdatePermissionInfo(UpdatePermissionInfoParam param);

        ResultInfo UpdateMarketInfoList(UpdateMarketInfoParam param);
    }
}
