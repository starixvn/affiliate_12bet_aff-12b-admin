using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.Configurations;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.Services.LandingPage;
using System.Web.Mvc;
using Fanex.Affiliate.Admin.Core.MetaModels.LandingPage;
using System.Web;

namespace Fanex.Affiliate.Admin.Core.Controllers.LandingPage
{
    [FunctionAuthorize(FunctionName = Function.RegistrationFormUploader)]
    public class FormManagementController : MediaUploaderController
    {
        public FormManagementController(
            ISessionService sessionService,
            IAppSetting appSetting,
            IAssetUploaderService assetService,
            ILandingPageService landingPageService) : base(sessionService, appSetting, assetService, landingPageService)
        {
        }

        [HttpGet]
        public override ActionResult Index()
        {
            return base.Index();
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetFormManagementList(AssetUploaderParam param)
        {
            param.FunctionName = Function.RegistrationFormUploader;
            param.ModuleIds = LandingPageConstants.ModuleIds.RegistrationForm.ToString();

            return base.GetMediaList(param);
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetFormDetail(AssetDetailParam param)
        {
            return base.GetMediaDetail(param);
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.RegistrationFormUploader, IsHost = true)]
        public ActionResult UploadRegistrationForms(HttpPostedFileBase[] files, UploadAssetsParam param)
        {
            param.FunctionName = Function.RegistrationFormUploader;

            return base.UploadMedias(files, param);
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.RegistrationFormUploader, IsHost = true)]
        public ActionResult UpdateRegistrationForm(HttpPostedFileBase files, UpdateAssetParam param)
        {
            param.FunctionName = Function.RegistrationFormUploader;

            return base.UpdateMedia(files, param);
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.RegistrationFormUploader, IsHost = true)]
        public ActionResult DeleteForms(DeleteMediasParam param)
        {
            param.FunctionName = Function.RegistrationFormUploader;

            return base.DeleteMedias(param);
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.RegistrationFormUploader, IsHost = true)]
        public ActionResult ToggleFormStatus(UpdateMediasStatusParam param)
        {
            param.FunctionName = Function.RegistrationFormUploader;

            return base.ToggleMediaStatus(param);
        }
    }
}