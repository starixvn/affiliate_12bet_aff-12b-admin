namespace Fanex.Affiliate.User.Core.BusinessService
{
    using Constants;
    using CoreService;
    using Criterias;
    using Data.Repository;
    using DTO;
    using DTO.UserPanel;
    using DTO.UserPanel.MediaGallery;
    using Extensions;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Threading;
    using System.Web;

    public class SystemService : BaseDynamicService, ISystemService
    {
        private readonly ISessionService _sessionService;
        private readonly IMemoryCacheService _cacheService;

        public SystemService(
            IDynamicRepository dynamicRepository,
            ISessionService sessionService,
            IMemoryCacheService cacheService) : base(dynamicRepository)
        {
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

        public IEnumerable<Language> GetSupportedLanguages()
        {
            return GetAllLanguages().Where(l => !l.Code.Equals("te-IN"));
        }

        public IEnumerable<Language> GetLanguageList()
        {
            var languageList = _cacheService.Get<IEnumerable<Language>>(CacheKey.LanguageList);

            if (languageList == null)
            {
                languageList = GetAllLanguages();
                _cacheService.Set(CacheKey.LanguageList, languageList);
            }

            return languageList;
        }

        private IEnumerable<Language> GetAllLanguages()
        {
            var isForInternal = _sessionService.IsInternal;
            var languages = FetchTData<LanguageType>(new GetLanguageListCriteria(isForInternal));

            return languages
                .Select(l => new Language
                {
                    Code = l.MarketId,
                    DisplayName = l.DisplayName,
                    EnglishName = l.MarketName,
                    NativeName = l.MarketNativeName,
                    ShortedCode = l.ShortedCode
                });
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

        public IEnumerable<Currency> GetBaseCurrencyList()
        {
            return FetchTData<Currency>(new GetBaseCurrencyListCriteria());
        }
    }
}
