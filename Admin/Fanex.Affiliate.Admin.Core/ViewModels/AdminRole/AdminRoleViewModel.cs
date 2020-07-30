using Fanex.Affiliate.Admin.Core.DTO;
using System.Collections.Generic;

namespace Fanex.Affiliate.Admin.Core.ViewModels.AdminRole
{
    public class AdminRoleViewModel
    {
        public bool IsBack { get; set; }

        public IEnumerable<SubMenu> SubMenuList { get; set; }
    }
}
