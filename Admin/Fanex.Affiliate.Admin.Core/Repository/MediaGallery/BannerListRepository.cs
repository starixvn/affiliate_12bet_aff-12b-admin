using System;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.MediaGallery;
using Fanex.Affiliate.Admin.Core.MetaModels.MediaGallery;

namespace Fanex.Affiliate.Admin.Core.Repository.MediaGallery
{
    public class BannerListRepository : BaseRepository, IBannerListRepository
    {
        public EntityResult<BannerInfo> GetBannerList(BannerParam param)
        {
            throw new NotImplementedException();
        }

        public BannerInfo GetBannerDetail(int selectedBannerId)
        {
            throw new NotImplementedException();
        }

        public ResultInfo InsertBanner(InsertUpdateBannerParam param)
        {
            throw new NotImplementedException();
        }

        public ResultInfo UpdateBanner(InsertUpdateBannerParam param)
        {
            throw new NotImplementedException();
        }

        public ResultInfo DeleteBanners(DeleteBannerParam param)
        {
            throw new NotImplementedException();
        }
    }
}
