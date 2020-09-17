using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.AdminRole;
using Fanex.Affiliate.Admin.Core.MetaModels.AdminRole;
using System.Collections.Generic;

namespace Fanex.Affiliate.Admin.Core.Services.AdminRole
{
    public interface IRolePermissionService
    {
        IEnumerable<RolePermissionInfo> GetPermissionInfoList();

        ResultInfo UpdatePermissionInfo(UpdateRolePermissionParam param);

        ResultInfo UpdateMarketInfoList(UpdateMarketInfoParam param);
    }
}
