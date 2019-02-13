using System.Web.Mvc;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.DTO;
using System.Collections.Generic;
using Fanex.Affiliate.Admin.Core.Resources;

namespace Fanex.Affiliate.Admin.Core.Controllers.UserPanel
{
    public class ClickStatisNewController : BaseController
    {
        public ClickStatisNewController(ISessionService sessionService) : base(sessionService)
        {
        }

        [HttpGet]
        public ActionResult Index (string fdate, string tdate, int affId, int slDevice = 0, int current = 0)
        {
            var subMenu = new List<SubMenu>
            {
                new SubMenu { Name = "Referrers", Url = Url.Content($"~/Portal/UserPanel/ClickStatisNew/Referrers?fdate={fdate}&tdate={tdate}&slDevice={slDevice}&affId={affId}") },
                new SubMenu { Name = "Langding", Url = Url.Content("~/Portal/LandingPage/Index") }
            };

            var activeIndex = current > subMenu.Count - 1 ? 0 : current;
            subMenu[activeIndex].IsActive = true;

            return View("~/Areas/Portal/Views/UserPanel/ClickStatisNew/Index.cshtml", subMenu);
        }

        [HttpGet]
        public ActionResult Referrers()
        {
            return View("~/Areas/Portal/Views/UserPanel/ClickStatisNew/Referrers.cshtml");
        }
    }
}
