using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.Configurations;
using System.Web;
using System.Web.Mvc;
using Fanex.Affiliate.Admin.Core.Services.LandingPage;
using Fanex.Affiliate.Admin.Core.ViewModels.LandingPage;
using Fanex.Affiliate.Admin.Core.MetaModels.LandingPage;

namespace Fanex.Affiliate.Admin.Core.Controllers.LandingPage
{
    [FunctionAuthorize(FunctionName = Function.LandingPage)]
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
                ModuleList = _landingPageService.GetModuleList(),
                LanguageList = _landingPageService.GetLanguageList(),
                TargetCountryList = _landingPageService.GetTargetCountryList(),
                BannerRootUrl = _appSetting.BannerRootUrl,
                MaximumFiles = LandingPageConstants.FileUpload.MaximumFiles,
                MaxFileSize = LandingPageConstants.FileUpload.MaxFileSizeBytes,
                AllowedExtensions = LandingPageConstants.FileExtensions.AllowedImageExtensions
            };

            return View("~/Areas/Portal/Views/LandingPage/AssetUploader.cshtml", model);
        }


        [HttpPost]
        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.LandingPage, IsHost = true)]
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
        public ActionResult GetAssetList(AssetListParam param)
        {
            return GenerateJsonNetResult(_assetService.GetAssetList(param));
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.LandingPage, IsHost = true)]
        public ActionResult DeleteAssets(DeleteAssetsParam param)
        {
            if (ModelState.IsValid)
            {
                AssignUserInfoParam(param);
                return GenerateJsonNetResult(_assetService.DeleteAssets(param));
            }
            return GenerateModelStateErrResult();
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

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetAssetStatistics(int? moduleId)
        {
            return GenerateJsonNetResult(_assetService.GetAssetStatistics(moduleId));
        }

        private UploadAssetsParam EncodeUploadAssetsParam(UploadAssetsParam param)
        {
            return new UploadAssetsParam
            {
                ModuleId = param.ModuleId,
                AssetType = param.AssetType,
                AssetName = Server.HtmlEncode(param.AssetName),
                AssetDescription = Server.HtmlEncode(param.AssetDescription),
                Tags = Server.HtmlEncode(param.Tags)
            };
        }
    }
}
