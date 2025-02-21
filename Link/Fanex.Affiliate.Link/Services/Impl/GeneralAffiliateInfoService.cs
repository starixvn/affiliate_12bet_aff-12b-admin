namespace Fanex.Affiliate.Link.Services.Impl
{
    using Fanex.Affiliate.Link.Criterias;
    using Fanex.Affiliate.Link.DTOs;
    using Fanex.Affiliate.Link.Services.Interface;
    using Fanex.Caching.Memory;
    using Fanex.Data.Repository;
    using System.Collections.Generic;

    public class GeneralAffiliateInfoService : IGeneralAffiliateInfoService
    {
        private const string CHANNEL_LIST_CACHE_KEY = "Affiliate.12B.Go.ChannelList";

        private readonly IDynamicRepository _dynamicRepository;
        private readonly IMemoryCacheService _memoryCacheService;

        public GeneralAffiliateInfoService()
            : this(null, null)
        {
        }

        public GeneralAffiliateInfoService(IDynamicRepository dynamicRepository, IMemoryCacheService memoryCacheService)
        {
            _dynamicRepository = dynamicRepository ?? new DynamicRepository();
            _memoryCacheService = memoryCacheService ?? new MemoryCacheService(new MemoryCache(new MemoryCacheOptions()));
        }

        public AffiliateInfo GetGeneralAffiliateInfo(int? affId = null, string affCode = null)
        {
            var criteria = new GeneralAffiliateInfoCriteria
            {
                AffId = affId,
                AffCode = affCode,
            };

            return _dynamicRepository.Get<AffiliateInfo>(criteria);
        }

        public CommGroup GetCommGroupByAffCode(string affCode)
        {
            var criteria = new CommGroupByAffCodeCriteria
            {
                AffCode = affCode,
            };

            return _dynamicRepository.Get<CommGroup>(criteria);
        }

        public IEnumerable<MarketingChannel> GetChannels(int userId)
        {
            var cacheKey = $"{CHANNEL_LIST_CACHE_KEY}_{userId}";
            var channels = _memoryCacheService.Get<IEnumerable<MarketingChannel>>(cacheKey);
            if (channels == null)
            {
                channels = _dynamicRepository.Fetch<MarketingChannel>(new MarketingChannelCriteria(userId));
                _memoryCacheService.Set(cacheKey, channels);
            }

            return channels ?? new List<MarketingChannel>();
        }
    }
}
