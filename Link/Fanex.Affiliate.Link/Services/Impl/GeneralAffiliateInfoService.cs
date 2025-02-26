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
        private const string COMM_GROUP_CACHE_KEY = "Affiliate.12B.Go.CommGroup";

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

            return _dynamicRepository.Get<AffiliateInfo>(criteria) ?? new AffiliateInfo();
        }

        public CommGroup GetCommGroupByAffCode(string affCode)
        {
            var cacheKey = $"{COMM_GROUP_CACHE_KEY}_{affCode}";
            var commGroup = _memoryCacheService.Get<CommGroup>(cacheKey);
            if (commGroup == null)
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
