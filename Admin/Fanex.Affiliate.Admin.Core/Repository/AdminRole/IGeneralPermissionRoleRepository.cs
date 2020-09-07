using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.AdminRole;
using Fanex.Affiliate.Admin.Core.MetaModels.AdminRole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fanex.Affiliate.Admin.Core.Repository.AdminRole
{
    public interface IGeneralPermissionRoleRepository
    {
        IEnumerable<PermissionInfo> GetPermissionInfoList();

        IEnumerable<MarketInfo> GetMarketByRoleList();

        ResultInfo UpdatePermissiontInfoList(UpdateMarketInfoParam param);
    }
}
