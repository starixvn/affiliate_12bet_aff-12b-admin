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
    public class AssetUploaderController : MediaUploaderController
    {
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
            param.FunctionName = Function.AssetUploader;

            return base.GetMediaList(param);
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetAssetDetail(AssetDetailParam param)
        {
            return base.GetMediaDetail(param);
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.AssetUploader, IsHost = true)]
        public ActionResult UploadAssets(HttpPostedFileBase[] files, UploadAssetsParam param)
        {
            param.FunctionName = Function.AssetUploader;

            return base.UploadMedias(files, param);
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.AssetUploader, IsHost = true)]
        public ActionResult UpdateAsset(HttpPostedFileBase files, UpdateAssetParam param)
        {
            param.FunctionName = Function.AssetUploader;

            return base.UpdateMedia(files, param);
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.AssetUploader, IsHost = true)]
        public ActionResult DeleteAssets(DeleteMediasParam param)
        {
            param.FunctionName = Function.AssetUploader;

            return base.DeleteMedias(param);
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.AssetUploader, IsHost = true)]
        public ActionResult ToggleAssetStatus(UpdateMediasStatusParam param)
        {
            param.FunctionName = Function.AssetUploader;

            return base.ToggleMediaStatus(param);
        }
    }
}
