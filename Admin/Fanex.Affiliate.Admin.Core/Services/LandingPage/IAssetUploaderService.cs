using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.LandingPage;
using Fanex.Affiliate.Admin.Core.MetaModels.LandingPage;
using System.Web;

namespace Fanex.Affiliate.Admin.Core.Services.LandingPage
{
    public interface IAssetUploaderService
    {
        ResultInfo UploadAssets(HttpPostedFileBase[] files, UploadAssetsParam param);
        ResultInfo UpdateAsset(HttpPostedFileBase file, UpdateAssetParam param);
        EntityResult<AssetDetail> GetAssetList(AssetUploaderParam param);
        AssetDetail GetAssetDetail(AssetDetailParam param);
        ResultInfo DeleteAssets(DeleteAssetsParam param);
        ResultInfo ToggleAssetStatus(UpdateAssetsStatusParam param);
    }
}
