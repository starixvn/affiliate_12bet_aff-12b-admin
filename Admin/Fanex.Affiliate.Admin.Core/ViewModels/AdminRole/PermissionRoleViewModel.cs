using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.AdminRole;
using System.Collections.Generic;

namespace Fanex.Affiliate.Admin.Core.ViewModels.AdminRole
{
    public class PermissionRoleViewModel
    {
        public int UserId { get; set; }

        public string UserName { get; set; }

        public IEnumerable<SubMenu> SubMenuList { get; set; }

        public IEnumerable<UserPermissionInfo> PermissionRoleInfoList { get; set; }
    }
}
