using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.MediaGallery;
using Fanex.Affiliate.Admin.Core.MetaModels.MediaGallery;

namespace Fanex.Affiliate.Admin.Core.Services.MediaGallery
{
    public interface IBannerListService
    {
        EntityResult<BannerInfo> GetBannerList(BannerParam param);

        BannerInfo GetBannerDetail(int selectedBannerId);

        ResultInfo InsertBanner(InsertUpdateBannerParam param);

        ResultInfo UpdateBanner(InsertUpdateBannerParam param);

        ResultInfo DeleteBanners(DeleteBannerParam param);
    }
}
