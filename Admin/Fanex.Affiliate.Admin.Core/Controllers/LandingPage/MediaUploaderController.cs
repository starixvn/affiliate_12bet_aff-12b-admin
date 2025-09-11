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
    public abstract class MediaUploaderController : ReadOnlySessionBaseController
    {
        private readonly IAppSetting _appSetting;
        private readonly IAssetUploaderService _assetService;
        private readonly ILandingPageService _landingPageService;

        protected MediaUploaderController(
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
                LanguageList = _landingPageService.GetLanguageList(),
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

        public ActionResult GetMediaList(AssetUploaderParam param)
        {
            param.UserId = SessionService.UserId;
            return GenerateJsonNetResult(_assetService.GetAssetList(param));
        }

        public ActionResult UploadMedias(HttpPostedFileBase[] files, UploadAssetsParam param)
        {
            if (ModelState.IsValid && files != null && files.Length > 0)
            {
                var encodeParam = EncodeMediaParam(param);
                AssignUserInfoParam(encodeParam);
                return GenerateJsonNetResult(_assetService.UploadAssets(files, encodeParam));
            }
            return GenerateErrResult("Please select valid files to upload.");
        }

        public ActionResult GetMediaDetail(AssetDetailParam param)
        {
            param.UserId = SessionService.UserId;
            return GenerateJsonNetResult(_assetService.GetAssetDetail(param));
        }

        public ActionResult UpdateMedia(HttpPostedFileBase files, UpdateAssetParam param)
        {
            if (ModelState.IsValid && (!string.IsNullOrEmpty(param.InputFileNames) || files != null))
            {
                var encodeParam = EncodeMediaParam(param);
                encodeParam.MediaId = param.MediaId;

                AssignUserInfoParam(encodeParam);
                return GenerateJsonNetResult(_assetService.UpdateAsset(files, encodeParam));
            }
            return GenerateErrResult("Please select valid  file to update.");
        }

        public ActionResult DeleteMedias(DeleteMediasParam param)
        {
            if (ModelState.IsValid)
            {
                param.UserId = SessionService.UserId;
                param.Ip = Request.GetClientIP();
                return GenerateJsonNetResult(_assetService.DeleteMedias(param));
            }
            return GenerateModelStateErrResult();
        }

        public ActionResult ToggleMediaStatus(UpdateMediasStatusParam param)
        {
            AssignUserInfoParam(param);

            return GenerateJsonNetResult(_assetService.ToggleAssetStatus(param));
        }

        private UpdateAssetParam EncodeMediaParam(UploadAssetsParam param)
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
    }
}
