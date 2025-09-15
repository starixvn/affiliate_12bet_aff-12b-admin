using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.Configurations;
using System.Web;
using System.Web.Mvc;
using Fanex.Affiliate.Admin.Core.Services.LandingPage;
using Fanex.Affiliate.Admin.Core.ViewModels.LandingPage;
using Fanex.Affiliate.Admin.Core.MetaModels.LandingPage;
using Fanex.Affiliate.Admin.Core.Extensions;

namespace Fanex.Affiliate.Admin.Core.Controllers.LandingPage
{
    public abstract class BaseAssetUploaderController : ReadOnlySessionBaseController
    {
        private readonly IAppSetting _appSetting;
        private readonly IAssetUploaderService _assetService;
        private readonly ILandingPageService _landingPageService;
        protected virtual string FunctionName => Function.DisabledFunction;

        protected BaseAssetUploaderController(
            ISessionService sessionService,
            IAppSetting appSetting,
            IAssetUploaderService assetService,
            ILandingPageService landingPageService) : base(sessionService)
        {
            _appSetting = appSetting;
            _assetService = assetService;
            _landingPageService = landingPageService;
        }

        public virtual ActionResult Index()
        {
            var model = new AssetUploaderViewModel
            {
                ModuleList = _landingPageService.GetModuleList(),
                AllLanguagesInSystem = _landingPageService.GetAllLanguagesInSystem(),
                TargetCountryList = _landingPageService.GetTargetCountryList(),
                StatusList = LandingPageConstants.AssetStatusList,
                BannerRootUrl = _appSetting.BannerRootUrl,
                MaximumFiles = LandingPageConstants.FileUpload.MaximumFiles,
                MaxFileSize = LandingPageConstants.FileUpload.MaxFileSizeMegabytes,
                AllowedExtensions = LandingPageConstants.FileExtensions.AllImageExtensions
            };

            return View("~/Areas/Portal/Views/LandingPage/AssetUploader.cshtml", model);
        }

        public ActionResult GetBaseAssetList(AssetUploaderParam param)
        {
            param.UserId = SessionService.UserId;
            param.FunctionName = FunctionName;

            return GenerateJsonNetResult(_assetService.GetAssetList(param));
        }

        public ActionResult GetBaseAssetDetail(AssetDetailParam param)
        {
            param.UserId = SessionService.UserId;
            return GenerateJsonNetResult(_assetService.GetAssetDetail(param));
        }

        public ActionResult UploadBaseAssets(HttpPostedFileBase[] files, UploadAssetsParam param)
        {
            if (ModelState.IsValid && files != null && files.Length > 0)
            {
                var encodeParam = EncodeBaseAssetParam(param);
                AssignBaseAssetUserInfoParam(encodeParam);

                return GenerateJsonNetResult(_assetService.UploadAssets(files, encodeParam));
            }
            return GenerateErrResult("Please select valid files to upload.");
        }

        public ActionResult UpdateBaseAsset(HttpPostedFileBase files, UpdateAssetParam param)
        {
            if (ModelState.IsValid && (!string.IsNullOrEmpty(param.InputFileNames) || files != null))
            {
                var encodeParam = EncodeBaseAssetParam(param);
                encodeParam.MediaId = param.MediaId;

                AssignBaseAssetUserInfoParam(encodeParam);

                return GenerateJsonNetResult(_assetService.UpdateAsset(files, encodeParam));
            }
            return GenerateErrResult("Please select valid  file to update.");
        }

        public ActionResult DeleteBaseAssets(DeleteAssetsParam param)
        {
            if (ModelState.IsValid)
            {
                AssignBaseAssetUserInfoParam(param);

                return GenerateJsonNetResult(_assetService.DeleteAssets(param));
            }
            return GenerateModelStateErrResult();
        }

        public ActionResult ToggleBaseAssetStatus(UpdateAssetsStatusParam param)
        {
            if (ModelState.IsValid)
            {
                AssignBaseAssetUserInfoParam(param);

                return GenerateJsonNetResult(_assetService.ToggleAssetStatus(param));
            }
            return GenerateModelStateErrResult();
        }

        private UpdateAssetParam EncodeBaseAssetParam(UploadAssetsParam param)
        {
            return new UpdateAssetParam
            {
                ModuleId = param.ModuleId,
                TargetCountryCode = Server.HtmlEncode(param.TargetCountryCode),
                MarketId = Server.HtmlEncode(param.MarketId),
                InputFileNames = Server.HtmlEncode(param.InputFileNames),
                InputMediaNames = Server.HtmlEncode(param.InputMediaNames)
            };
        }

        protected void AssignBaseAssetUserInfoParam(BaseAssetParam param)
        {
            param.FunctionName = FunctionName;
            AssignUserInfoParam(param);
        }
    }
}
