using System.Collections.Generic;
using Fanex.Affiliate.User.Core.Constants;
using Fanex.Affiliate.User.Core.DTO.UserPanel;
using Fanex.Affiliate.User.Core.Repository.UserPanel;
using Fanex.Affiliate.User.Core.Utilities;

namespace Fanex.Affiliate.User.Core.BusinessService
{
    public class MarketingService : IMarketingService
    {
        private readonly IMarketingRepository _marketingRepository;

        private readonly IMemoryCacheService _cacheService;

        public MarketingService(IMarketingRepository marketingRepository, IMemoryCacheService cacheService)
        {
            _marketingRepository = marketingRepository;
            _cacheService = cacheService;
        }

        public IEnumerable<BannerType> GetBannerTypeList(bool isInternal)
        {
            var bannerTypeList = _cacheService.Get<IEnumerable<BannerType>>(CacheKey.BannerTypeList);
            if (bannerTypeList == null)
            {
                bannerTypeList = _marketingRepository.GetBannerTypeList(isInternal);
            }

            _cacheService.Set(CacheKey.BannerTypeList, bannerTypeList);

            return bannerTypeList;
        }
    }
}