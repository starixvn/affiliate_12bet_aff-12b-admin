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
        public ActionResult GetAssetList(AssetUploaderParam param)
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
                var encodeParam = EncodeAssetParam(param);
                AssignUserInfoParam(encodeParam);
                return GenerateJsonNetResult(_assetService.UploadAssets(files, encodeParam));
            }
            return GenerateErrResult("Please select valid files to upload.");
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetAssetDetail(AssetDetailParam param)
        {
            param.UserId = SessionService.UserId;
            return GenerateJsonNetResult(_assetService.GetAssetDetail(param));
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.AssetUploader, IsHost = true)]
        public ActionResult UpdateAsset(HttpPostedFileBase files, UpdateAssetParam param)
        {
            if (ModelState.IsValid && (!string.IsNullOrEmpty(param.InputFileNames) || files != null))
            {
                var encodeParam = EncodeAssetParam(param);
                encodeParam.MediaId = param.MediaId;

                AssignUserInfoParam(encodeParam);
                return GenerateJsonNetResult(_assetService.UpdateAsset(files, encodeParam));
            }
            return GenerateErrResult("Please select valid  file to update.");
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

        private UpdateAssetParam EncodeAssetParam(UploadAssetsParam param)
        {
            return new UpdateAssetParam
            {
                ModuleId = param.ModuleId,
                TargetCountryCode = Server.HtmlEncode(param.TargetCountryCode),
                MarketId = Server.HtmlEncode(param.MarketId),
                InputFileNames = Server.HtmlEncode(param.InputFileNames),
                InputMediaNames = Server.HtmlEncode(param.InputMediaNames),
                FunctionName = Function.AssetUploader
            };
        }
    }
}
