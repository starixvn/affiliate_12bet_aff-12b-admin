using System.Web.Mvc;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.DTO;
using System.Collections.Generic;
using Fanex.Affiliate.Admin.Core.Resources;

namespace Fanex.Affiliate.Admin.Core.Controllers.UserPanel
{
    public class ClickStatisticsNewController : BaseController
    {
        public ClickStatisticsNewController(ISessionService sessionService) : base(sessionService)
        {
        }

        [HttpGet]
        public ActionResult Index (string fromDate, string toDate, int affId, int deviceType = 0, int current = 0)
        {
            var subMenu = new List<SubMenu>
            {
                new SubMenu { Name = Lang.Referrers, Url = Url.Content($"~/Portal/UserPanel/Referrers/Index?fromDate={fromDate}&toDate={toDate}&deviceType={deviceType}&affId={affId}") },
                new SubMenu { Name = Lang.LangdingPages, Url = Url.Content("~/Portal/LandingPage/Index") }
            };

            var activeIndex = current > subMenu.Count - 1 ? 0 : current;
            subMenu[activeIndex].IsActive = true;

            return View("~/Areas/Portal/Views/UserPanel/ClickStatisticsNew/Index.cshtml", subMenu);
        }
    }
}
