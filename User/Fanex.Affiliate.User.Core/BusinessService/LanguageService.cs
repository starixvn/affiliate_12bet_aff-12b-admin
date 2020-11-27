using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using Fanex.Affiliate.User.Core.Constants;
using Fanex.Affiliate.User.Core.DTO;
using Fanex.Affiliate.User.Core.CoreService;
using Fanex.Affiliate.User.Core.Repository;

namespace Fanex.Affiliate.User.Core.BusinessService
{
    public class LanguageService : ILanguageService
    {
        private readonly ISessionService _sessionService;
        private readonly ILanguageRepository _languageRepository;
        private readonly IMemoryCacheService _cacheService;

        public LanguageService(ILanguageRepository languageRepository, ISessionService sessionService, IMemoryCacheService cacheService)
        {
            _languageRepository = languageRepository;
            _sessionService = sessionService;
            _cacheService = cacheService;
        }

        public string GetFavoriteLanguage()
        {
            var preferedUrlLang = HttpContext.Current.Request["lang"]?.Trim();
            var preferedLang = SystemLanguage.SupportedLanguage.FirstOrDefault(i => string.Equals(i.Value, preferedUrlLang, StringComparison.OrdinalIgnoreCase)).Key;

            if (IsValid(preferedLang))
            {
                return preferedLang;
            }

            var langCookie = HttpContext.Current.Request.Cookies["afffavoritelang"]?.Value;

            return IsValid(langCookie) ? langCookie : SystemLanguage.DefaultLanguageCode;
        }

        public IEnumerable<Language> GetSystemLanguages(bool isAfterLogin = true)
        {
            var isInternal = _sessionService.IsInternal;
            var cacheKey = isAfterLogin && isInternal ? CacheKey.InternalLanguageList : CacheKey.LanguageList;

            var languageList = _cacheService.Get<IEnumerable<Language>>(cacheKey);
            languageList = languageList ?? _languageRepository.GetLanguageList(isInternal, _sessionService.UserSession?.UserId);

            if (languageList != null)
            {
                _cacheService.Set(cacheKey, languageList);
            }

            return languageList ?? new List<Language>();
        }

        public bool IsValid(string langCode, bool isAfterLogin = true)
            => !string.IsNullOrWhiteSpace(langCode) && GetSystemLanguages(isAfterLogin).Any(l => Equals(l.MarketId, langCode));


        public void SetCulture(string languageCode)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(languageCode);
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(languageCode);

            Thread.CurrentThread.CurrentCulture.DateTimeFormat = CultureInfo.CreateSpecificCulture("en-US").DateTimeFormat;
            Thread.CurrentThread.CurrentCulture.NumberFormat = CultureInfo.CreateSpecificCulture("en-US").NumberFormat;
        }
    }
}
