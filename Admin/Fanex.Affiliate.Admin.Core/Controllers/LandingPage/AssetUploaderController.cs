using Fanex.Affiliate.Admin.Core.Attributes;
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
    [FunctionAuthorize(FunctionName = Function.AssetUploader)]
    public class AssetUploaderController : ReadOnlySessionBaseController
    {
        private readonly IAppSetting _appSetting;
        private readonly IAssetUploaderService _assetService;
        private readonly ILandingPageService _landingPageService;

        public AssetUploaderController(
            ISessionService sessionService,
            IAppSetting appSetting,
            IAssetUploaderService assetService,
            ILandingPageService landingPageService) : base(sessionService)
        {
            _appSetting = appSetting;
            _assetService = assetService;
            _landingPageService = landingPageService;
        }
        [HttpGet]
        public ActionResult Index()
        {
            var model = new AssetUploaderViewModel
            {
                ModuleList = _landingPageService.GetModuleListExcludeRegistrationForm(),
                LanguageList = _landingPageService.GetLanguageList(),
                TargetCountryList = _landingPageService.GetTargetCountryList(),
                StatusList = LandingPageConstants.AssetStatusList,
                BannerRootUrl = _appSetting.BannerRootUrl,
                MaximumFiles = LandingPageConstants.FileUpload.MaximumFiles,
                MaxFileSize = LandingPageConstants.FileUpload.MaxFileSizeMegabytes,
                AllowedExtensions = LandingPageConstants.FileExtensions.AllImageExtensions
            };

            return View("~/Areas/Portal/Views/LandingPage/AssetUploader.cshtml", model);
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetAssetList(AssetListParam param)
        {
            param.FunctionName = Function.AssetUploader;
            param.UserId = SessionService.UserId;
            return GenerateJsonNetResult(_assetService.GetAssetList(param));
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.AssetUploader, IsHost = true)]
        public ActionResult UploadAssets(HttpPostedFileBase[] files, UploadAssetsParam param)
        {
            if (ModelState.IsValid && files != null && files.Length > 0)
            {
                var encodeParam = EncodeUploadAssetsParam(param);
                AssignUserInfoParam(encodeParam);
                return GenerateJsonNetResult(_assetService.UploadAssets(files, encodeParam));
            }
            return GenerateErrResult("Please select valid files to upload.");
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetAssetDetail(AssetListParam param)
        {
            param.FunctionName = Function.AssetUploader;
            param.UserId = SessionService.UserId;
            return GenerateJsonNetResult(_assetService.GetAssetDetail(param));
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.AssetUploader, IsHost = true)]
        public ActionResult UpdateAsset(HttpPostedFileBase file, UploadAssetsParam param)
        {
            if (ModelState.IsValid && (!string.IsNullOrEmpty(param.InputFileNames) || file != null))
            {
                var encodeParam = EncodeUploadAssetsParam(param);
                AssignUserInfoParam(encodeParam);
                return GenerateJsonNetResult(_assetService.UpdateAsset(file, encodeParam));
            }
            return GenerateErrResult("Please select valid files to upload.");
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.AssetUploader, IsHost = true)]
        public ActionResult DeleteAssets(DeleteMediasParam param)
        {
            if (ModelState.IsValid)
            {
                param.FunctionName = Function.AssetUploader;
                param.UserId = SessionService.UserId;
                param.Ip = Request.GetClientIP();
                return GenerateJsonNetResult(_assetService.DeleteMedias(param));
            }
            return GenerateModelStateErrResult();
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.AssetUploader, IsHost = true)]
        public ActionResult ToggleAssetStatus(UpdateMediasStatusParam param)
        {
            param.FunctionName = Function.AssetUploader;
            AssignUserInfoParam(param);

            return GenerateJsonNetResult(_assetService.ToggleAssetStatus(param));
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult OptimizeAsset(OptimizeAssetParam param)
        {
            if (ModelState.IsValid)
            {
                return GenerateJsonNetResult(_assetService.OptimizeAsset(param));
            }
            return GenerateModelStateErrResult();
        }

        private UploadAssetsParam EncodeUploadAssetsParam(UploadAssetsParam param)
        {
            return new UploadAssetsParam
            {
                MediaId = param.MediaId,
                ModuleId = param.ModuleId,
                TargetCountryCode = Server.HtmlEncode(param.TargetCountryCode),
                MarketId = Server.HtmlEncode(param.MarketId),
                InputFileNames = param.InputFileNames,
                FunctionName = Function.AssetUploader
            };
        }
    }
}
