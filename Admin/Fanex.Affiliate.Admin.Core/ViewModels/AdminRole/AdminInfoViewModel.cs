using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.AdminRole;
using System.Collections.Generic;

namespace Fanex.Affiliate.Admin.Core.ViewModels.AdminRole
{
    public class AdminInfoViewModel
    {
        public string UserName { get; set; }

        public AdminInfo UserInfo { get; set; }

        public IEnumerable<SubMenu> SubMenuList { get; set; }
    }
}
