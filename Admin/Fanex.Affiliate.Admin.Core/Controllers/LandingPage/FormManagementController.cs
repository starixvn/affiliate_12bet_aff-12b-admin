using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.Configurations;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.Services.LandingPage;
using Fanex.Affiliate.Admin.Core.ViewModels.LandingPage;
using System.Web.Mvc;
using Fanex.Affiliate.Admin.Core.MetaModels.LandingPage;
using Fanex.Affiliate.Admin.Core.MetaModels;

namespace Fanex.Affiliate.Admin.Core.Controllers.LandingPage
{
    [FunctionAuthorize(FunctionName = Function.RegistrationFormUploader)]
    public class FormManagementController : ReadOnlySessionBaseController
    {
        private readonly IAppSetting _appSetting;
        private readonly ILandingPageService _landingPageService;
        private readonly IAssetUploaderService _assetService;

        public FormManagementController(
            ISessionService sessionService,
            IAppSetting appSetting,
            ILandingPageService landingPageService,
            IAssetUploaderService assetService) : base(sessionService)
        {
            _appSetting = appSetting;
            _landingPageService = landingPageService;
            _assetService = assetService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var model = new FormManagementViewModel
            {
                FormNameList = _landingPageService.GetModuleList(),
                TargetCountryList = _landingPageService.GetTargetCountryList(),
                LanguageList = _landingPageService.GetLanguageList(),
                StatusList = LandingPageConstants.AssetStatusList,
                BannerRootUrl = _appSetting.BannerRootUrl,
            };

            return View("~/Areas/Portal/Views/LandingPage/FormManagement.cshtml", model);
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetFormManagementList(PagingParam param)
        {
            var queryParam = new AssetListParam
            {
                FunctionName = Function.RegistrationFormUploader,
                UserId = SessionService.UserId,
                ModuleIds = LandingPageConstants.ModuleIds.RegistrationForm.ToString(),
                PageIndex = param.PageIndex,
                PageSize = param.PageSize
            };

            return GenerateJsonNetResult(_assetService.GetAssetList(queryParam));
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.RegistrationFormUploader, IsHost = true)]
        public ActionResult ToggleFormStatus(UpdateMediasStatusParam param)
        {
            param.FunctionName = Function.RegistrationFormUploader;
            AssignUserInfoParam(param);

            return GenerateJsonNetResult(_assetService.ToggleAssetStatus(param));
        }
    }
}