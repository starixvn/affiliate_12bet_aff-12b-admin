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
            if ((string.Compare(param.Category, "Casino", StringComparison.CurrentCultureIgnoreCase) == 0
                || string.Compare(param.Category, "Sportsbook", StringComparison.CurrentCultureIgnoreCase) == 0) && string.IsNullOrEmpty(param.SubCategory))
            {
                return _bannerListRepository.GetBannerList(param, "Aff_BannerSelByParentCate");
            }

            if (!string.IsNullOrEmpty(param.SubCategory))
            {
                param.Category = param.SubCategory;
            }

            return _bannerListRepository.GetBannerList(param, "Aff_BannerSel_1");
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
