using System.Collections.Generic;
using System.Web;
using Fanex.Affiliate.Admin.Core.DTO;

namespace Fanex.Affiliate.Admin.Core.Constants
{
    public static class UserPanelConstant
    {
        public static IEnumerable<SubMenu> UserPanelMenuList => new List<SubMenu>
        {
            new SubMenu { Name = "Statement History", Url = VirtualPathUtility.ToAbsolute("~/UserInfo/EarningsHistory.aspx") },
            new SubMenu { Name = "Edit", Url = VirtualPathUtility.ToAbsolute("~/UserInfo/AffiliateUpd.aspx") },
            new SubMenu { Name = "Note", Url = VirtualPathUtility.ToAbsolute("~/UserInfo/Note.aspx") },
            new SubMenu { Name = "Players Detail", Url = VirtualPathUtility.ToAbsolute("~/Portal/UserPanel/PlayerDetail/Index") },
            new SubMenu { Name = "Adjustment", Url = VirtualPathUtility.ToAbsolute("~/UserInfo/Adjustment.aspx") },
            new SubMenu { Name = "Tracking Statistics", Url = VirtualPathUtility.ToAbsolute("~/UserInfo/MediaStatistics.aspx") },
            new SubMenu { Name = "Member", Url = VirtualPathUtility.ToAbsolute("~/Portal/UserPanel/CustomerMember/Index") },
            new SubMenu { Name = "View log", Url = VirtualPathUtility.ToAbsolute("~/UserInfo/UserLog.aspx") },
            new SubMenu { Name = "Click statis New", Url = VirtualPathUtility.ToAbsolute("~/UserInfo/MediaStatistics_New.aspx") }
        };
    }
}