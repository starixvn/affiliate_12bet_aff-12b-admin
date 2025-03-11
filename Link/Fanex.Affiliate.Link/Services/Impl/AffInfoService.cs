namespace Fanex.Affiliate.Link.Services.Impl
{
    using Fanex.Affiliate.Link.Constants;
    using Fanex.Affiliate.Link.Criterias;
    using Fanex.Affiliate.Link.DTOs;
    using Fanex.Affiliate.Link.Services.Interface;
    using Fanex.Data.Repository;

    public class AffInfoService : IAffInfoService
    {
        private readonly IDynamicRepository _dynamicRepository;
        private readonly IMemoryCacheService _memoryCacheService;

        public AffInfoService()
            : this(null, null)
        {
        }

        public AffInfoService(IDynamicRepository dynamicRepository, IMemoryCacheService memoryCacheService)
        {
            _dynamicRepository = dynamicRepository ?? new DynamicRepository();
            _memoryCacheService = memoryCacheService ?? new MemoryCacheService();
        }

        public AffiliateInfo GetAffInfoByAffId(int affId)
        {
            var cacheKey = $"{CacheKey.AFF_INFO_BY_AFF_ID_CACHE_KEY}_{affId}";
            var affInfo = _memoryCacheService.Get<AffiliateInfo>(cacheKey);
            if (affInfo is null)
            {
                var criteria = new GeneralAffiliateInfoCriteria
                {
                    AffId = affId,
                };

                affInfo = _dynamicRepository.Get<AffiliateInfo>(criteria);

                _memoryCacheService.Set(cacheKey, affInfo);
            }

            return affInfo ?? new AffiliateInfo();
        }

        public AffiliateInfo GetAffInfoByAffCode(string affCode)
        {
            var cacheKey = $"{CacheKey.AFF_INFO_BY_AFF_CODE_CACHE_KEY}_{affCode}";
            var affInfo = _memoryCacheService.Get<AffiliateInfo>(cacheKey);
            if (affInfo is null)
            {
                var criteria = new GeneralAffiliateInfoCriteria
                {
                    AffCode = affCode,
                };

                affInfo = _dynamicRepository.Get<AffiliateInfo>(criteria);

                _memoryCacheService.Set(cacheKey, affInfo);
            }

            return affInfo ?? new AffiliateInfo();
        }

        public CommGroup GetCommGroupByAffCode(string affCode)
        {
            var cacheKey = $"{CacheKey.COMM_GROUP_CACHE_KEY}_{affCode}";
            var commGroup = _memoryCacheService.Get<CommGroup>(cacheKey);
            if (commGroup is null)
            {
                var criteria = new CommGroupByAffCodeCriteria
                {
                    AffCode = affCode,
                };

                commGroup = _dynamicRepository.Get<CommGroup>(criteria);

                _memoryCacheService.Set(cacheKey, commGroup);
            }

            return commGroup ?? new CommGroup();
        }
    }
}
