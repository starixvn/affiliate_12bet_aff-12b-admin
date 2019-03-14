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

        public ActionResult Index(int current = 0)
        {
            var subMenu = new List<SubMenu>
            {
                new SubMenu { Name = Lang.AffiliateInfo, Url = Url.Content("~/Portal/AffiliateInfo/Index") },
                new SubMenu { Name = Lang.PaymentRequest, Url = Url.Content("~/Payment/PaymentRequest.aspx") },
                new SubMenu { Name = Lang.PaymentRequest, Url = Url.Content("~/Portal/PaymentRequest/Index") },
                new SubMenu { Name = Lang.CommGroup, Url = Url.Content("~/Portal/CommissionGroup") },
                new SubMenu { Name = Lang.EmailHistory, Url = Url.Content("~/Others/EmailHistory.aspx") }
            };

            var activeIndex = current > subMenu.Count - 1 ? 0 : current;
            subMenu[activeIndex].IsActive = true;

            return View(subMenu);
        }
    }
}
