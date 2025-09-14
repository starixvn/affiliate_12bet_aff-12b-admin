using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.Configurations;
using System.Web;
using System.Web.Mvc;
using Fanex.Affiliate.Admin.Core.Services.LandingPage;
using Fanex.Affiliate.Admin.Core.MetaModels.LandingPage;

namespace Fanex.Affiliate.Admin.Core.Controllers.LandingPage
{
    [FunctionAuthorize(FunctionName = Function.AssetUploader)]
    public class AssetUploaderController : BaseAssetUploaderController
    {
        protected override string FunctionName => Function.AssetUploader;

        public AssetUploaderController(
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
        public ActionResult GetAssetList(AssetUploaderParam param)
        {
            return GetBaseAssetList(param);
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetAssetDetail(AssetDetailParam param)
        {
            return GetBaseAssetDetail(param);
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.AssetUploader, IsHost = true)]
        public ActionResult UploadAssets(HttpPostedFileBase[] files, UploadAssetsParam param)
        {
            return UploadBaseAssets(files, param);
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.AssetUploader, IsHost = true)]
        public ActionResult UpdateAsset(HttpPostedFileBase files, UpdateAssetParam param)
        {
            return UpdateBaseAsset(files, param);
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.AssetUploader, IsHost = true)]
        public ActionResult DeleteAssets(DeleteAssetsParam param)
        {
            return DeleteBaseAssets(param);
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.AssetUploader, IsHost = true)]
        public ActionResult ToggleAssetStatus(UpdateAssetsStatusParam param)
        {
            return ToggleBaseAssetStatus(param);
        }
    }
}
