using Fanex.Affiliate.Admin.Core.DTO.AdminRole;
using System.Collections.Generic;

namespace Fanex.Affiliate.Admin.Core.Services.AdminRole
{
    public interface IPermissionRoleService
    {
        IEnumerable<PermissionRoleInfo> GetPermissionRoleList(string userName, bool isGeneral = true);
    }
}
