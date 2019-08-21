using System.Collections.Generic;
using System.Web;
using Fanex.Affiliate.Admin.Core.DTO;

namespace Fanex.Affiliate.Admin.Core.Constants
{
    public static class UserPanelConstant
    {
        public static IEnumerable<SubMenu> UserPanelMenuList => new List<SubMenu>
        {
            new SubMenu { Name = "Statement History", Url = VirtualPathUtility.ToAbsolute("~/Portal/UserPanel/StatementHistory/Index") },
            new SubMenu { Name = "Edit", Url = VirtualPathUtility.ToAbsolute("~/UserInfo/AffiliateUpd.aspx") },
            new SubMenu { Name = "Note", Url = VirtualPathUtility.ToAbsolute("~/Portal/UserPanel/Note/Index") },
            new SubMenu { Name = "Players Detail", Url = VirtualPathUtility.ToAbsolute("~/Portal/UserPanel/PlayerDetail/Index") },
            new SubMenu { Name = "Adjustment", Url = VirtualPathUtility.ToAbsolute("~/Portal/UserPanel/Adjustment/Index") },
            new SubMenu { Name = "Tracking Statistics", Url = VirtualPathUtility.ToAbsolute("~/Portal/UserPanel/TrackingStatistics/Index") },
            new SubMenu { Name = "Member", Url = VirtualPathUtility.ToAbsolute("~/Portal/UserPanel/CustomerMember/Index") },
            new SubMenu { Name = "View log", Url = VirtualPathUtility.ToAbsolute("~/Portal/UserPanel/ViewLog/Index") },
            new SubMenu { Name = "Click statis New", Url = VirtualPathUtility.ToAbsolute("~/Portal/UserPanel/Referrers/Index") },
            new SubMenu { Name = "Edit New", Url = VirtualPathUtility.ToAbsolute("~/Portal/UserPanel/UserAccount/Index") }
        };
    }
}