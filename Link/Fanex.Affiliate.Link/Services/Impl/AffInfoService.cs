namespace Fanex.Affiliate.Link.Services.Impl
{
    using Fanex.Affiliate.Link.Constants;
    using Fanex.Affiliate.Link.Criterias;
    using Fanex.Affiliate.Link.DTOs;
    using Fanex.Affiliate.Link.Services.Interface;
    using Fanex.Caching;
    using Fanex.Data.Repository;
    using StackExchange.Redis;

    public class AffInfoService(IDynamicRepository dynamicRepository,
            IMemoryCacheService memoryCacheService,
            ConnectionMultiplexer connection,
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

            var x = connection.GetDatabase(5).StringIncrementAsync("ClickId");
            
            if (masterInfoValidations is null)
            {
                var criteria = new GetMasterDataForValidateClickedCriteria();
                try
                {
                    masterInfoValidations = (await dynamicRepository.FetchAsync<MasterInfoValidation>(criteria)).ToList();
                    await cacheService.SetAsync(cacheKey, masterInfoValidations, new CacheItemOptions
                    {
                        AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(CacheDurationInSeconds)
                    });
                }
                catch (Exception ex)
                {
                    masterInfoValidations = new List<MasterInfoValidation>();

                    await cacheService.SetAsync(cacheKey, masterInfoValidations, new CacheItemOptions
                    {
                        AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(60)
                    });
                }


            }

            return masterInfoValidations ?? new List<MasterInfoValidation>();
        }


        ///// <summary>
        ///// Increments the value of a given clickId key in Redis by 1.
        ///// </summary>
        ///// <param name="clickId">The clickId to increment.</param>
        ///// <returns>The new value after increment.</returns>
        //public async Task<long> IncrementClickIdAsync(string clickId)
        //{
        //    if (string.IsNullOrWhiteSpace(clickId))
        //    {
        //        throw new ArgumentException("clickId cannot be null or empty.", nameof(clickId));
        //    }

        //    // Assume _redisConnection is a ConnectionMultiplexer injected via constructor
        //    // var db = _redisConnection.GetDatabase();
        //    var redisKey = $"ClickId:{clickId}";
        //    return await db.StringIncrementAsync(redisKey);
        //}

        ///// <summary>
        ///// Increments the value of a given clickId key in Redis by 1 using a direct connection.
        ///// </summary>
        ///// <param name="redisConnectionString">The Redis connection string.</param>
        ///// <param name="clickId">The clickId to increment.</param>
        ///// <returns>The new value after increment.</returns>
        //public async Task<long> IncrementClickIdByConnectionAsync(string redisConnectionString, string clickId)
        //{
        //    if (string.IsNullOrWhiteSpace(redisConnectionString))
        //    {
        //        throw new ArgumentException("Redis connection string cannot be null or empty.", nameof(redisConnectionString));
        //    }
        //    if (string.IsNullOrWhiteSpace(clickId))
        //    {
        //        throw new ArgumentException("clickId cannot be null or empty.", nameof(clickId));
        //    }

        //    // Connect to Redis using the provided connection string
        //    using (var connection = await StackExchange.Redis.ConnectionMultiplexer.ConnectAsync(redisConnectionString))
        //    {
        //        var db = connection.GetDatabase();
        //        var redisKey = $"ClickId:{clickId}";
        //        return await db.StringIncrementAsync(redisKey);
        //    }
        //}

        /// <summary>
        /// Gets the Redis database instance from the provided connection multiplexer.
        /// </summary>
        /// <param name="connection">The ConnectionMultiplexer instance.</param>
        /// <param name="db">The database number to select. Default is -1 (use default database).</param>
        /// <returns>IDatabase instance for Redis operations.</returns>
        public StackExchange.Redis.IDatabase GetRedisDatabase(StackExchange.Redis.ConnectionMultiplexer connection, int db = 5)
        {
            if (connection == null || !connection.IsConnected)
            {
               
                throw new InvalidOperationException("Redis connection is not available.");
            }

            try
            {
                return connection.GetDatabase(db);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to get Redis database instance.", ex);
            }
        }
    }
}
