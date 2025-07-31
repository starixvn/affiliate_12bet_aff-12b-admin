namespace Fanex.Affiliate.Link.Services.Impl
{
    using System.Threading.Tasks;
    using Fanex.Affiliate.Link.Constants;
    using Fanex.Affiliate.Link.Criterias;
    using Fanex.Affiliate.Link.DTOs;
    using Fanex.Affiliate.Link.Services.Interface;
    using Fanex.Caching;
    using Fanex.Caching.Redis;
    using Fanex.Data.Repository;
    using StackExchange.Redis;

    public class AffInfoService(IDynamicRepository dynamicRepository,
            IMemoryCacheService memoryCacheService,
            ICacheService cacheService) : IAffInfoService
    {

        private const int CacheDurationInSeconds = 86400;

        public AffiliateInfo GetAffInfoByAffId(int affId)
        {
            var cacheKey = $"{CacheKey.AFF_INFO_BY_AFF_ID_CACHE_KEY}_{affId}";
            var affInfo = memoryCacheService.Get<AffiliateInfo>(cacheKey);
            if (affInfo is null)
            {
                var criteria = new GeneralAffiliateInfoCriteria
                {
                    AffId = affId,
                };

                affInfo = dynamicRepository.Get<AffiliateInfo>(criteria);

                memoryCacheService.Set(cacheKey, affInfo);
            }

            return affInfo ?? new AffiliateInfo();
        }

        public AffiliateInfo GetAffInfoByAffCode(string affCode)
        {
            var cacheKey = $"{CacheKey.AFF_INFO_BY_AFF_CODE_CACHE_KEY}_{affCode}";
            var affInfo = memoryCacheService.Get<AffiliateInfo>(cacheKey);
            if (affInfo is null)
            {
                var criteria = new GeneralAffiliateInfoCriteria
                {
                    AffCode = affCode,
                };

                affInfo = dynamicRepository.Get<AffiliateInfo>(criteria);

                memoryCacheService.Set(cacheKey, affInfo);
            }

            return affInfo ?? new AffiliateInfo();
        }

        public CommGroup GetCommGroupByAffCode(string affCode)
        {
            var cacheKey = $"{CacheKey.COMM_GROUP_CACHE_KEY}_{affCode}";
            var commGroup = memoryCacheService.Get<CommGroup>(cacheKey);
            if (commGroup is null)
            {
                var criteria = new CommGroupByAffCodeCriteria
                {
                    AffCode = affCode,
                };

                commGroup = dynamicRepository.Get<CommGroup>(criteria);

                memoryCacheService.Set(cacheKey, commGroup);
            }

            return commGroup ?? new CommGroup();
        }

        public async Task<IList<MasterInfoValidation>> GetMasterDataForValidateClicked()
        {
            var cacheKey = CacheKey.MASTER_DATA;
            var masterInfoValidations = await cacheService.GetAsync<IList<MasterInfoValidation>>(cacheKey);
            var redisCache = (RedisCache)cacheService.CacheStorage;

            if (masterInfoValidations is null)
            {
                var criteria = new GetMasterDataForValidateClickedCriteria();
                masterInfoValidations = (await dynamicRepository.FetchAsync<MasterInfoValidation>(criteria)).ToList();
                await cacheService.SetAsync(cacheKey, masterInfoValidations, new CacheItemOptions
                {
                    AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(CacheDurationInSeconds)
                });
            }

            return masterInfoValidations ?? new List<MasterInfoValidation>();
        }

        public async Task<long> GetClickId()
        {
            var redisCache = (RedisCache)cacheService.CacheStorage;
            var clickIdFromCache = await redisCache.StringGetAsync(CacheKey.ClickId);
            if (clickIdFromCache is null)
            {
                var clickIdFromDatabase = await GetMaxClickIdFromDatabase();
                await redisCache.StringSetAsync(CacheKey.ClickId, clickIdFromDatabase.ToString());
            }
            
            var result = await redisCache.StringIncrementAsync(CacheKey.ClickId);
            return result;
        }

        private async Task<long> GetMaxClickIdFromDatabase()
        {
            var criteria = new GetMaxClickIdCriteria();
            var clickId = await dynamicRepository.GetAsync<long>(criteria);
            return clickId;
        }
    }
}
