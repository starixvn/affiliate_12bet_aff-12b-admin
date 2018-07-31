using System.Collections.Generic;
using System.Web.Mvc;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.Resources;
using Fanex.Affiliate.Admin.Core.Services.Common;

namespace Fanex.Affiliate.Admin.Core.Controllers
{
    public class MarketingController : BaseController
    {
        public MarketingController(ISessionService sessionService) : base(sessionService)
        {
        }

        public ActionResult Index()
        {
            var subMenu = new List<SubMenu>
            {
                new SubMenu { Name = Lang.TextList, Url = Url.Content("~/Media/ListText.aspx") },
                new SubMenu { Name = Lang.BannerList, Url = Url.Content("~/Media/ListBannerNew.aspx") }
            };

            return View(subMenu);
        }
    }
}
