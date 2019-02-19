using System.Collections.Generic;
using Fanex.Affiliate.Admin.Core.Services.Common;
using System.Web.Mvc;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.Resources;
using Fanex.Affiliate.Admin.Core.Services;
using Fanex.Affiliate.Admin.Core.ViewModels.UserPanel;
using Fanex.Affiliate.Admin.Core.MetaModels.UserPanel;
using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Repository.UserPanel;
using Fanex.Affiliate.Admin.Core.Extensions;

namespace Fanex.Affiliate.Admin.Core.Controllers.UserPanel
{
    public class LandingPagesController : BaseController
    {
        private readonly ILanguageService _languageService;
        private readonly ILandingPagesRepository _landingPagesRepository;
        public LandingPagesController(
            ISessionService sessionService,
            ILanguageService languageService,
            ILandingPagesRepository landingPagesRepository) : base(sessionService)
        {
            _languageService = languageService;
            _landingPagesRepository = landingPagesRepository;
        }

        [HttpGet]
        public ActionResult Index(AffiliateLinkParam param)
        {
            param.AffId = param.AffId == 0 ? SessionService.AffId : param.AffId;
            var subMenu = new List<SubMenu>
            {
                new SubMenu { Name = Lang.Referrers, Url = Url.Content($"~/Portal/UserPanel/Referrers/Index?affId={param.AffId}&affName={param.AffName}") },
                new SubMenu { Name = Lang.LandingPages, Url = Url.Content($"~/Portal/UserPanel/LandingPages/Index?affId={param.AffId}&affName={param.AffName}") }
            };

            subMenu[1].IsActive = true;

            var viewModel = new LandingPagesViewModel
            {
                SubMenuList = subMenu,
                LanguageList = _languageService.GetLanguageList(),
                AffiliateLinkParam = param
            };

            return View("~/Areas/Portal/Views/UserPanel/LandingPages/Index.cshtml", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [Compress]
        public ActionResult GetLandingPagesList(LandingPagesParam param)
        {
            param.AffId = param.AffId == 0 ? SessionService.AffId : param.AffId;

            var model = _landingPagesRepository.GetLandingPagesList(param);

            return new JsonNetResult
            {
                Data = model
            };
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [Compress]
        public ActionResult GetLandingPagesDetailList(LandingPagesDetailParam param)
        {
            param.AffId = param.AffId == 0 ? SessionService.AffId : param.AffId;

            var model = _landingPagesRepository.GetLandingPagesDetailList(param);

            return new JsonNetResult
            {
                Data = model
            };
        }
    }
}
