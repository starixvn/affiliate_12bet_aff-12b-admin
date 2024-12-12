using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using Fanex.Affiliate.User.Core.Constants;
using Fanex.Affiliate.User.Core.CoreService;
using Fanex.Affiliate.User.Core.DTO;
using Fanex.Affiliate.User.Core.Extensions;
using Fanex.Affiliate.User.Core.Repository;

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
            var preferedLang = SystemLanguage.SupportedFavoriteLanguages.FirstOrDefault(i => i.Key.MatchWith(preferedUrlLang)).Value;

            if (IsValid(preferedLang))
            {
                return preferedLang;
            }

            var langCookie = HttpContext.Current.Request.Cookies["afffavoritelang"]?.Value;

            return IsValid(langCookie) ? langCookie : _sessionService.CurrentLanguage;
        }

        public string GetFavoriteShortedLanguage()
        {
            var favoriteLanguage = GetFavoriteLanguage();

            return SystemLanguage.GetCurrentShortedLanguage(favoriteLanguage);
        }

        public IEnumerable<Language> GetLanguageList(bool? isInternal = null)
        {
            var isForInternal = isInternal ?? _sessionService.IsInternal;
            var languageList = _cacheService.Get<IEnumerable<Language>>(CacheKey.LanguageList);

            if (languageList == null)
            {
                languageList = _systemRepository.GetLanguageList(isForInternal).Where(l => !l.Code.Equals("hi") && !l.Code.Equals("te-IN"));
                _cacheService.Set(CacheKey.LanguageList, languageList);
            }

            return languageList ?? new List<Language>();
        }

        public bool IsValid(string langCode)
            => !string.IsNullOrWhiteSpace(langCode) && GetLanguageList().Any(l => Equals(l.Code, langCode));

        public void SetCulture(string languageCode)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(languageCode);
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(languageCode);

            Thread.CurrentThread.CurrentCulture.DateTimeFormat = CultureInfo.CreateSpecificCulture("en-US").DateTimeFormat;
            Thread.CurrentThread.CurrentCulture.NumberFormat = CultureInfo.CreateSpecificCulture("en-US").NumberFormat;
        }
    }
}
