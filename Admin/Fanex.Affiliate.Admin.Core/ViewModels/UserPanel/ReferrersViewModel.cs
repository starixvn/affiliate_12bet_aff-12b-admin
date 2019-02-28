using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.MetaModels.UserPanel;
using System.Collections.Generic;

namespace Fanex.Affiliate.Admin.Core.ViewModels.UserPanel
{
    public class ReferrersViewModel
    {
        public IEnumerable<SubMenu> SubMenuList { get; set; }

        public ReferrersParam ReferrersParam { get; set; }
    }
}
