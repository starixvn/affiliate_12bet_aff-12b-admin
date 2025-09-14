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
    public class FormManagementController : BaseAssetUploaderController
    {
        protected override string FunctionName => Function.RegistrationFormUploader;

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
            param.ModuleIds = LandingPageConstants.ModuleIds.RegistrationForm.ToString();

            return GetBaseAssetList(param);
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetFormDetail(AssetDetailParam param)
        {
            return GetBaseAssetDetail(param);
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.RegistrationFormUploader, IsHost = true)]
        public ActionResult UploadRegistrationForms(HttpPostedFileBase[] files, UploadAssetsParam param)
        {
            return UploadBaseAssets(files, param);
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.RegistrationFormUploader, IsHost = true)]
        public ActionResult UpdateRegistrationForm(HttpPostedFileBase files, UpdateAssetParam param)
        {
            return UpdateBaseAsset(files, param);
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.RegistrationFormUploader, IsHost = true)]
        public ActionResult DeleteForms(DeleteAssetsParam param)
        {
            return DeleteBaseAssets(param);
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.RegistrationFormUploader, IsHost = true)]
        public ActionResult ToggleFormStatus(UpdateAssetsStatusParam param)
        {
            return ToggleBaseAssetStatus(param);
        }
    }
}