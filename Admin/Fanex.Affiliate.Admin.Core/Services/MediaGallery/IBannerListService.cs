using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.MediaGallery;
using Fanex.Affiliate.Admin.Core.MetaModels.MediaGallery;
using System.Collections.Generic;
using System.Web;

namespace Fanex.Affiliate.Admin.Core.Services.MediaGallery
{
    public interface IBannerListService
    {
        EntityResult<BannerInfo> GetBannerList(BannerParam param);

        BannerInfo GetBannerDetail(int selectedBannerId);

        ResultInfo InsertBanner(HttpPostedFileBase file, InsertUpdateBannerParam param);

        ResultInfo UpdateBanner(HttpPostedFileBase file, InsertUpdateBannerParam param);

        ResultInfo DeleteBanners(DeleteBannerParam param);

        IEnumerable<MediaGalleryFilterItem> GetSizeList(SizeParam param);
    }
}
