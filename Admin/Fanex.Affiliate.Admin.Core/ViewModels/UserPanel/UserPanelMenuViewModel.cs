using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.MetaModels.UserPanel;
using System.Collections.Generic;

namespace Fanex.Affiliate.Admin.Core.ViewModels.UserPanel
{
    public class UserPanelMenuViewModel
    {
        public IEnumerable<SubMenu> SubMenuList { get; set; }

        public AffiliateLinkParam AffiliateLinkParam { get; set; }
    }
}
