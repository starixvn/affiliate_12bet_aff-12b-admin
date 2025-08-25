using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.LandingPage;
using Fanex.Affiliate.Admin.Core.MetaModels.LandingPage;
using System.Web;

namespace Fanex.Affiliate.Admin.Core.Services.LandingPage
{
    public interface IAssetUploaderService
    {
        ResultInfo UploadAssets(HttpPostedFileBase[] files, UploadAssetsParam param);
        EntityResult<LandingPageAsset> GetAssetList(AssetListParam param);
        ResultInfo DeleteAssets(DeleteAssetsParam param);
        OptimizedAssetResult OptimizeAsset(OptimizeAssetParam param);
        AssetStatistics GetAssetStatistics(int? moduleId);
    }
}
