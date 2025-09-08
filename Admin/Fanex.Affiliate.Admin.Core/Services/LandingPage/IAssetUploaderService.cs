using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.LandingPage;
using Fanex.Affiliate.Admin.Core.MetaModels.LandingPage;
using System.Web;

namespace Fanex.Affiliate.Admin.Core.Services.LandingPage
{
    public interface IAssetUploaderService
    {
        ResultInfo UploadAssets(HttpPostedFileBase[] files, UploadAssetsParam param);
        ResultInfo UpdateAsset(HttpPostedFileBase file, UploadAssetsParam param);
        EntityResult<LandingPageAsset> GetAssetList(AssetListParam param);
        LandingPageAsset GetAssetDetail(AssetListParam param);
        ResultInfo DeleteMedias(DeleteMediasParam param);
        ResultInfo ToggleAssetStatus(UpdateMediasStatusParam param);
        OptimizedAssetResult OptimizeAsset(OptimizeAssetParam param);
        AssetStatistics GetAssetStatistics(int? moduleId);
    }
}
