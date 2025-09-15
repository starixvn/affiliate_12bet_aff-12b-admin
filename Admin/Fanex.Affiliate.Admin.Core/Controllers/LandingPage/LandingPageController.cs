using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Configurations;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.Services.LandingPage;
using Fanex.Affiliate.Admin.Core.ViewModels.LandingPage;
using System.Web.Mvc;

namespace Fanex.Affiliate.Admin.Core.Controllers.LandingPage
{
    [FunctionAuthorize(FunctionName = Function.LandingPage)]
    public class LandingPageController : ReadOnlySessionBaseController
    {
        private readonly IAppSetting _appSetting;
        private readonly ILandingPageService _landingPageService;
        private readonly IPageManagementService _pageManagementService;

        public LandingPageController(
            ISessionService sessionService,
            IAppSetting appSetting,
            ILandingPageService landingPageService,
            IPageManagementService pageManagementService) : base(sessionService)
        {
            _appSetting = appSetting;
            _landingPageService = landingPageService;
            _pageManagementService = pageManagementService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var model = new PageManagementViewModel
            {
                LandingPageUrl = _appSetting.LandingPageUrl,
                CampaignList = _landingPageService.GetCampaignInfoList(),
                AllLanguagesInSystem = _landingPageService.GetAllLanguagesInSystem(),
                DealList = _landingPageService.GetMarketingDealList(),
                TargetCountryList = _landingPageService.GetTargetCountryList(),
                StatusList = _landingPageService.GetLandingPageStatusList(),
            };

            return View(model);
        }
    }
}