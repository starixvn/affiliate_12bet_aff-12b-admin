using System.Web.Mvc;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.DTO;
using System.Collections.Generic;
using Fanex.Affiliate.Admin.Core.Resources;
using Fanex.Affiliate.Admin.Core.MetaModels.UserPanel;

namespace Fanex.Affiliate.Admin.Core.Controllers.UserPanel
{
    public class ClickStatisticsNewController : BaseController
    {
        public ClickStatisticsNewController(ISessionService sessionService) : base(sessionService)
        {
        }

        [HttpGet]
        public ActionResult Index (string fdate, string tdate, int affId, int slDevice = 0, int current = 0)
        {
            var subMenu = new List<SubMenu>
            {
                new SubMenu { Name = "Referrers", Url = Url.Content($"~/Portal/UserPanel/ClickStatisticsNew/Referrers?fdate={fdate}&tdate={tdate}&slDevice={slDevice}&affId={affId}") },
                new SubMenu { Name = "Langding Pages", Url = Url.Content("~/Portal/LandingPage/Index") }
            };

            var activeIndex = current > subMenu.Count - 1 ? 0 : current;
            subMenu[activeIndex].IsActive = true;

            return View("~/Areas/Portal/Views/UserPanel/ClickStatisticsNew/Index.cshtml", subMenu);
        }

        [HttpGet]
        public ActionResult Referrers(ReferrersParam param)
        {
            return View("~/Areas/Portal/Views/UserPanel/ClickStatisticsNew/Referrers.cshtml", param);
        }
    }
}
