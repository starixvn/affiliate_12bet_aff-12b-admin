using Fanex.Affiliate.Link.DataAccess.Impl;
using Fanex.Affiliate.Link.DataAccess.Interface;
using Fanex.Affiliate.Link.DTO;
using Fanex.Affiliate.Link.Services.Interface;
using Fanex.Caching.Memory;
using System.Collections.Generic;

namespace Fanex.Affiliate.Link.Services.Impl
{
    public class AffiliateService : IAffiliateService
    {
        private const string TARGET_COUNTRY_LIST_CACHE_KEY = "Affiliate.12B.Go.TargetCountryList";

        private readonly IAffiliateDataAccess _affiliateDataAccess;
        private readonly IMemoryCacheService _cacheService;

        public AffiliateService() : this(null, null)
        {
        }

        private AffiliateService(IAffiliateDataAccess affiliateDataAccess, IMemoryCacheService cacheService)
        {
            _affiliateDataAccess = affiliateDataAccess ?? new AffiliateDataAccess();
            _cacheService = cacheService ?? new MemoryCacheService(new MemoryCache(new MemoryCacheOptions()));
        }

        public string GetCommGroupByAffId(int affId)
        {
            return _affiliateDataAccess.GetCommGroupByAffId(affId);
        }

        public string GetLanguageCodeByAffId(int affId)
        {
            return _affiliateDataAccess.GetLanguageCodeByAffId(affId);
        }

        public IEnumerable<TargetCountry> GetSystemTargetCountries()
        {
            var targetCountryList = _cacheService.Get<IEnumerable<TargetCountry>>(TARGET_COUNTRY_LIST_CACHE_KEY);
            if (targetCountryList == null)
            {
                targetCountryList = _affiliateDataAccess.GetSystemTargetCountries();
                _cacheService.Set(TARGET_COUNTRY_LIST_CACHE_KEY, targetCountryList);
            }

            return targetCountryList ?? new List<TargetCountry>();
        }
    }
}