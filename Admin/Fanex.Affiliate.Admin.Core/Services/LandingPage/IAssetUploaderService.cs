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
        EntityResult<LandingPageAsset> GetAssetList(AssetUploaderParam param);
        LandingPageAsset GetAssetDetail(AssetDetailParam param);
        ResultInfo DeleteMedias(DeleteMediasParam param);
        ResultInfo ToggleAssetStatus(UpdateMediasStatusParam param);
    }
}
