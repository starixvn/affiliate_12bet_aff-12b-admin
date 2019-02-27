using System.Collections.Generic;
using System.Web.Mvc;
using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.Resources;
using Fanex.Affiliate.Admin.Core.Services.Common;

namespace Fanex.Affiliate.Admin.Core.Controllers
{
    [FunctionAuthorize(FunctionName = Function.Media)]
    public class MediaGalleryController : BaseController
    {
        public MediaGalleryController(ISessionService sessionService) : base(sessionService)
        {
        }

        public ActionResult Index(int current = 0)
        {
            var subMenu = new List<SubMenu>
            {
                new SubMenu { Name = Lang.BannerList, Url = Url.Content("~/Media/ListBannerNew.aspx") },
                new SubMenu { Name = Lang.TextList, Url = Url.Content("~/Media/ListText.aspx") },
                new SubMenu { Name = "Landing Page List", Url = Url.Content("~/Portal/LandingPage/Index") }
            };

            var activeIndex = current > subMenu.Count - 1 ? 0 : current;
            subMenu[activeIndex].IsActive = true;

            return View(subMenu);
        }
    }
}
