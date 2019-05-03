using System.Collections.Generic;
using System.Web.Mvc;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.Resources;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.MetaModels.Payment;

namespace Fanex.Affiliate.Admin.Core.Controllers
{
    public class AffiliateController : BaseController
    {
        public AffiliateController(ISessionService sessionService) : base(sessionService)
        {
        }

        [HttpGet]
        public ActionResult Index(ProcessingPaymentRequestParam param, int current = 0)
        {
            var subMenu = new List<SubMenu>
            {
                new SubMenu { Name = Lang.AffiliateInfo, Url = Url.Content("~/Portal/AffiliateInfo/Index") },
                new SubMenu { Name = Lang.PaymentRequest, Url = Url.Content($"~/Portal/PaymentRequest/Index?SearchBy={param.SearchBy}&Status={param.Status}&FromDate={param.FromDate}") },
                new SubMenu { Name = Lang.CommGroup, Url = Url.Content("~/Portal/CommissionGroup") },
                new SubMenu { Name = Lang.EmailHistory, Url = Url.Content("~/Others/EmailHistory.aspx") }
            };

            var activeIndex = current > subMenu.Count - 1 ? 0 : current;
            subMenu[activeIndex].IsActive = true;

            return View(subMenu);
        }
    }
}
