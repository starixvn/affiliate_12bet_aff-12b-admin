using System.Collections.Generic;
using System.Web.Mvc;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.Resources;
using Fanex.Affiliate.Admin.Core.Services.Common;

namespace Fanex.Affiliate.Admin.Core.Controllers
{
    public class AffiliateController : BaseController
    {
        public AffiliateController(ISessionService sessionService) : base(sessionService)
        {
        }

        public ActionResult Index()
        {
            var subMenu = new List<SubMenu>
            {
                new SubMenu { Name = Lang.MyFavorite, Url = Url.Content("~/UserInfo/AffiliateList.aspx?status=approved&favor=1&isclk=1") },
                new SubMenu { Name = Lang.Approved, Url = Url.Content("~/UserInfo/AffiliateList.aspx?status=approved") },
                new SubMenu { Name = Lang.Pending, Url = Url.Content("~/UserInfo/AffiliateList.aspx?status=pending") },
                new SubMenu { Name = Lang.Disapproved, Url = Url.Content("~/UserInfo/AffiliateList.aspx?status=disapproved") },
                new SubMenu { Name = Lang.Closed, Url = Url.Content("~/UserInfo/AffiliateList.aspx?status=approved&isClosed=true") },
                new SubMenu { Name = Lang.PaymentRequest, Url = Url.Content("~/Payment/PaymentRequest.aspx") },
                new SubMenu { Name = Lang.CommGroup, Url = Url.Content("~/GroupSetting/GroupSetting.aspx") },
                new SubMenu { Name = Lang.EmailHistory, Url = Url.Content("~/Others/EmailHistory.aspx") }
            };

            return View(subMenu);
        }
    }
}
