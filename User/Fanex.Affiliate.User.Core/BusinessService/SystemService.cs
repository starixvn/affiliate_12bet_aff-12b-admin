using Fanex.Affiliate.User.Core.Constants;
using Fanex.Affiliate.User.Core.CoreService;
using Fanex.Affiliate.User.Core.DTO.System;
using Fanex.Affiliate.User.Core.Extensions;
using Fanex.Affiliate.User.Core.Repository;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;

namespace Fanex.Affiliate.User.Core.BusinessService
{
    public class SystemService : ISystemService
    {
        private readonly ISessionService _sessionService;
        private readonly ISystemRepository _systemRepository;
        private readonly IMemoryCacheService _cacheService;

        public SystemService(
            ISystemRepository systemRepository,
            ISessionService sessionService,
            IMemoryCacheService cacheService)
        {
            _systemRepository = systemRepository;
            _sessionService = sessionService;
            _cacheService = cacheService;
        }

        public string GetFavoriteLanguage()
        {
            var preferedUrlLang = HttpContext.Current.Request.RequestContext.RouteData.Values["lang"]?.ToString();
            var preferedLang = SystemLanguage.SupportedLanguage.FirstOrDefault(i => i.Value.MatchWith(preferedUrlLang)).Key;

            if (IsValid(preferedLang))
            {
                return preferedLang;
            }

            var langCookie = HttpContext.Current.Request.Cookies["afffavoritelang"]?.Value;

            return IsValid(langCookie) ? langCookie : SystemLanguage.DefaultLanguageCode;
        }

        public IEnumerable<TargetCountry> GetSystemTargetCountries()
        {
            var isInternal = _sessionService.IsInternal;
            var cacheKey = isInternal ? CacheKey.InternalTargetCountryList : CacheKey.TargetCountryList;

            var targetCountryList = _cacheService.Get<IEnumerable<TargetCountry>>(cacheKey);
            if (targetCountryList == null)
            {
                targetCountryList = _systemRepository.GetTargetCountryList(isInternal, _sessionService.UserSession?.UserId);
                _cacheService.Set(cacheKey, targetCountryList);
            }

            return targetCountryList ?? new List<TargetCountry>();
        }

        public IEnumerable<TargetLanguage> GetSystemTargetLanguages()
        {
            var isInternal = _sessionService.IsInternal;
            var cacheKey = isInternal ? CacheKey.InternalTargetLanguageList : CacheKey.TargetLanguageList;


            var targetLanguageList = _cacheService.Get<IEnumerable<TargetLanguage>>(cacheKey);
            if (targetLanguageList == null)
            {
                targetLanguageList = GetAllSystemTargetLanguages().Where(l => !l.LanguageCode.Equals("hi") && !l.LanguageCode.Equals("te-IN"));
                _cacheService.Set(cacheKey, targetLanguageList);
            }

            return targetLanguageList ?? new List<TargetLanguage>();
        }

        public IEnumerable<TargetLanguage> GetAllSystemTargetLanguages()
        {
            var isInternal = _sessionService.IsInternal;
            var cacheKey = isInternal ? CacheKey.InternalAllTargetLanguageList : CacheKey.AllTargetLanguageList;


            var targetLanguageList = _cacheService.Get<IEnumerable<TargetLanguage>>(cacheKey);
            if (targetLanguageList == null)
            {
                targetLanguageList = _systemRepository.GetTargetLanguageListByCountry(isInternal, userId: _sessionService.UserSession?.UserId);
                _cacheService.Set(cacheKey, targetLanguageList);
            }

            return targetLanguageList ?? new List<TargetLanguage>();
        }

        public IEnumerable<TargetLanguage> GetSystemTargetLanguagesByCountry(string targetCountry)
        {
            return _systemRepository.GetTargetLanguageListByCountry(_sessionService.IsInternal, targetCountry, _sessionService.UserSession?.UserId);
        }

        public bool IsValid(string langCode, bool isAfterLogin = true)
            => !string.IsNullOrWhiteSpace(langCode) && GetSystemTargetLanguages().Any(l => Equals(l.LanguageCode, langCode));


        public void SetCulture(string languageCode)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(languageCode);
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(languageCode);

            Thread.CurrentThread.CurrentCulture.DateTimeFormat = CultureInfo.CreateSpecificCulture("en-US").DateTimeFormat;
            Thread.CurrentThread.CurrentCulture.NumberFormat = CultureInfo.CreateSpecificCulture("en-US").NumberFormat;
        }
    }
}
