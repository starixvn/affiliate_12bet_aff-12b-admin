using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.MetaModels.AssetModification;
using Fanex.Affiliate.Admin.Core.Services.AssetModification;
using Fanex.Affiliate.Admin.Core.Services.Common;
using System.Web;
using System.Web.Mvc;

namespace Fanex.Affiliate.Admin.Core.Controllers
{
    [FunctionAuthorize(FunctionName = Function.PageManagement)]
    public class AssetModificationController : ReadOnlySessionBaseController
    {
        private readonly IAssetModificationService _assetModificationService;

        public AssetModificationController(
            ISessionService sessionService,
            IAssetModificationService assetModificationService) : base(sessionService)
        {
            _assetModificationService = assetModificationService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.DisabledFunction, IsHost = true)]
        public ActionResult AddAssets(HttpPostedFileBase[] files, AddAssetsParam param)
        {
            if (ModelState.IsValid && files != null && files.Length > 0)
            {
                AssignUserInfoParam(param);

                return GenerateJsonNetResult(_assetModificationService.UploadAssets(files, param));
            }
            return GenerateErrResult("Please select valid files to upload.");
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.PageManagement, IsHost = true)]
        public ActionResult DeleteAsset(DeleteAssetParam param)
        {
            if (ModelState.IsValid)
            {
                AssignUserInfoParam(param);

                return GenerateJsonNetResult(_assetModificationService.DeleteAsset(param));
            }
            return GenerateModelStateErrResult();
        }
    }
}
