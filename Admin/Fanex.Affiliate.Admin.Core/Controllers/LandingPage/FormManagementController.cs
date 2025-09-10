using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.Configurations;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.Services.LandingPage;
using Fanex.Affiliate.Admin.Core.ViewModels.LandingPage;
using System.Web.Mvc;
using Fanex.Affiliate.Admin.Core.MetaModels.LandingPage;
using Fanex.Affiliate.Admin.Core.MetaModels;
using System.Web;
using Fanex.Affiliate.Admin.Core.Extensions;

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
            var model = new AssetUploaderViewModel
            {
                ModuleList = _landingPageService.GetModuleList(),
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
        public ActionResult GetFormManagementList(AssetUploaderParam param)
        {
            param.ModuleIds = LandingPageConstants.ModuleIds.RegistrationForm.ToString();
            param.UserId = SessionService.UserId;
            param.FunctionName = Function.RegistrationFormUploader;

            return GenerateJsonNetResult(_assetService.GetAssetList(param));
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetFormDetail(AssetDetailParam param)
        {
            param.UserId = SessionService.UserId;
            return GenerateJsonNetResult(_assetService.GetAssetDetail(param));
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.RegistrationFormUploader, IsHost = true)]
        public ActionResult UploadRegistrationForms(HttpPostedFileBase[] files, UploadAssetsParam param)
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
        [FunctionAuthorize(FunctionName = Function.RegistrationFormUploader, IsHost = true)]
        public ActionResult UpdateRegistrationForm(HttpPostedFileBase files, UpdateAssetParam param)
        {
            if (ModelState.IsValid && (!string.IsNullOrEmpty(param.InputFileNames) || files != null))
            {
                var encodeParam = EncodeAssetParam(param);
                encodeParam.MediaId = param.MediaId;

                AssignUserInfoParam(encodeParam);
                return GenerateJsonNetResult(_assetService.UpdateAsset(files, encodeParam));
            }
            return GenerateErrResult("Please select valid file to update.");
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.RegistrationFormUploader, IsHost = true)]
        public ActionResult DeleteForms(DeleteMediasParam param)
        {
            if (ModelState.IsValid)
            {
                param.FunctionName = Function.RegistrationFormUploader;
                param.UserId = SessionService.UserId;
                param.Ip = Request.GetClientIP();
                return GenerateJsonNetResult(_assetService.DeleteMedias(param));
            }
            return GenerateModelStateErrResult();
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

        private UpdateAssetParam EncodeAssetParam(UploadAssetsParam param)
        {
            return new UpdateAssetParam
            {
                ModuleId = param.ModuleId,
                TargetCountryCode = Server.HtmlEncode(param.TargetCountryCode),
                MarketId = Server.HtmlEncode(param.MarketId),
                InputFileNames = Server.HtmlEncode(param.InputFileNames),
                InputMediaNames = Server.HtmlEncode(param.InputMediaNames),
                FunctionName = Function.RegistrationFormUploader
            };
        }
    }
}