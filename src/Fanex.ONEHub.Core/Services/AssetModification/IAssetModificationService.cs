using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.MetaModels.AssetModification;
using System.Web;

namespace Fanex.Affiliate.Admin.Core.Services.AssetModification
{
    public interface IAssetModificationService
    {
        ResultInfo UploadAssets(HttpPostedFileBase[] files, AddAssetsParam param);
        ResultInfo DeleteAsset(DeleteAssetParam param);
    }
}
