using Fanex.Affiliate.Link.DataAccess.Impl;
using Fanex.Affiliate.Link.DataAccess.Interface;
using Fanex.Affiliate.Link.DTO;
using Fanex.Affiliate.Link.Services.Interface;
using Fanex.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fanex.Affiliate.Link.Services.Impl
{
    public class AffiliateService : IAffiliateService
    {
        private const string LANGUAGE_LIST_CACHE_KEY = "Affiliate.12B.Go.LanguageList";

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

        public string GetTargetCountryCodeByLanguage(string shortedCode)
        {
            var systemLanguages = GetSystemLanguages();

            return systemLanguages.FirstOrDefault(l => string.Equals(shortedCode, l.ShortedCode, StringComparison.OrdinalIgnoreCase))?.TargetCountryCode ?? string.Empty;
        }

        public IEnumerable<Language> GetSystemLanguages()
        {
            var systemLanguages = _cacheService.Get<IEnumerable<Language>>(LANGUAGE_LIST_CACHE_KEY);
            if (systemLanguages == null)
            {
                systemLanguages = _affiliateDataAccess.GetSystemLanguages();
                _cacheService.Set(LANGUAGE_LIST_CACHE_KEY, systemLanguages);
            }

            return systemLanguages ?? new List<Language>();
        }
    }
}