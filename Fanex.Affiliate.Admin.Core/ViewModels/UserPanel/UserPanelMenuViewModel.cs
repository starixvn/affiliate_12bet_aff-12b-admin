using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Affiliate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fanex.Affiliate.Admin.Core.ViewModels.UserPanel
{
    public class UserPanelMenuViewModel
    {
        public IEnumerable<SubMenu> SubMenuList { get; set; }

        public AffiliateLinkParam AffiliateLinkParam { get; set; }
    }
}
