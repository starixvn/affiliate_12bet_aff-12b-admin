using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.Configurations;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.Services.LandingPage;
using Fanex.Affiliate.Admin.Core.ViewModels.LandingPage;
using System.Web.Mvc;

namespace Fanex.Affiliate.Admin.Core.Controllers.LandingPage
{
    [FunctionAuthorize(FunctionName = Function.LandingPage)]
    public class FormManagementController : ReadOnlySessionBaseController
    {
        private readonly IAppSetting _appSetting;
        private readonly ILandingPageService _landingPageService;

        public FormManagementController(
            ISessionService sessionService,
            IAppSetting appSetting,
            ILandingPageService landingPageService) : base(sessionService)
        {
            _appSetting = appSetting;
            _landingPageService = landingPageService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var model = new FormManagementViewModel
            {
                TargetCountryList = _landingPageService.GetTargetCountryList(),
                LanguageList = _landingPageService.GetLanguageList(),
                BannerRootUrl = _appSetting.BannerRootUrl,
            };

            return View("~/Areas/Portal/Views/LandingPage/FormManagement.cshtml", model);
        }
    }
}