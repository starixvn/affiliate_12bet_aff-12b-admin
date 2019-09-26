using System;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.MediaGallery;
using Fanex.Affiliate.Admin.Core.MetaModels.MediaGallery;
using Fanex.Affiliate.Admin.Core.Repository.MediaGallery;

namespace Fanex.Affiliate.Admin.Core.Services.MediaGallery
{
    public class BannerListService : IBannerListService
    {
        private readonly IBannerListRepository _bannerListRepository;
        public BannerListService(IBannerListRepository bannerListRepository)
        {
            _bannerListRepository = bannerListRepository;
        }

        public EntityResult<BannerInfo> GetBannerList(BannerParam param)
        {
            return _bannerListRepository.GetBannerList(param);
        }

        public BannerInfo GetBannerDetail(int selectedBannerId)
        {
            return _bannerListRepository.GetBannerDetail(selectedBannerId);
        }

        public ResultInfo InsertBanner(InsertUpdateBannerParam param)
        {
            return _bannerListRepository.InsertBanner(param);
        }

        public ResultInfo UpdateBanner(InsertUpdateBannerParam param)
        {
            return _bannerListRepository.UpdateBanner(param);
        }

        public ResultInfo DeleteBanners(DeleteBannerParam param)
        {
            return _bannerListRepository.DeleteBanners(param);
        }
    }
}
