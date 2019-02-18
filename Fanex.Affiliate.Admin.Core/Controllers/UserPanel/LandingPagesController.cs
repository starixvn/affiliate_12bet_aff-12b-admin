using System.Collections.Generic;
using Fanex.Affiliate.Admin.Core.Services.Common;
using System.Web.Mvc;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.Resources;
using Fanex.Affiliate.Admin.Core.Services;
using Fanex.Affiliate.Admin.Core.ViewModels.UserPanel;

namespace Fanex.Affiliate.Admin.Core.Controllers.UserPanel
{
    public class LandingPagesController : BaseController
    {
        private readonly ILanguageService _languageService;
        public LandingPagesController(
            ISessionService sessionService,
            ILanguageService languageService) : base(sessionService)
        {
            _languageService = languageService;
        }

        [HttpGet]
        public ActionResult Index(int affId, string affName)
        {
            var subMenu = new List<SubMenu>
            {
                new SubMenu { Name = Lang.Referrers,
                    Url = Url.Content($"~/Portal/UserPanel/Referrers/Index?affId={(affId == 0 ? SessionService.AffId : affId)}&affName={affName}") },
                new SubMenu { Name = Lang.LandingPages,
                    Url = Url.Content($"~/Portal/UserPanel/LandingPages/Index?affId={(affId == 0 ? SessionService.AffId : affId)}&affName={affName}") }
            };

            subMenu[1].IsActive = true;

            var viewModel = new LandingPagesViewModel
            {
                SubMenuList = subMenu,
                LanguageList = _languageService.GetLanguageList()
            };

            return View("~/Areas/Portal/Views/UserPanel/LandingPages/Index.cshtml", viewModel);
        }
    }
}
