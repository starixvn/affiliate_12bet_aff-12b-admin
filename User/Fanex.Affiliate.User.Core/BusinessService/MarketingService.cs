using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Fanex.Affiliate.User.Core.Constants;
using Fanex.Affiliate.User.Core.CoreService;
using Fanex.Affiliate.User.Core.DTO.UserPanel;
using Fanex.Affiliate.User.Core.Extensions;
using Fanex.Affiliate.User.Core.MetalModels.UserPanel;
using Fanex.Affiliate.User.Core.Repository.UserPanel;
using Fanex.Affiliate.User.Core.Resources;
using Fanex.Affiliate.User.Core.DTO.UserPanel.MediaGallery;
using Fanex.Affiliate.User.Core.DTO;

namespace Fanex.Affiliate.User.Core.BusinessService
{
    public class MarketingService : IMarketingService
    {
        private readonly IMarketingRepository _marketingRepository;

        private readonly IMemoryCacheService _cacheService;
        private readonly ILanguageService _languageService;

        public MarketingService(IMarketingRepository marketingRepository
            , IMemoryCacheService cacheService
            , ILanguageService languageService)
        {
            _marketingRepository = marketingRepository;
            _cacheService = cacheService;
            _languageService = languageService;
        }

        public IEnumerable<CategoryType> GetCategoryList(bool isMediaPage, bool isInternal)
        {
            var cacheKey = isMediaPage ? CacheKey.BannerCategoryList : CacheKey.TextCategoryList;
            var categoryList = _cacheService.Get<IEnumerable<CategoryType>>(cacheKey);
            if (categoryList == null)
            {
                categoryList = _marketingRepository.GetCategoryList(isMediaPage, isInternal);

                _cacheService.Set(cacheKey, categoryList);
            }

            return categoryList ?? new List<CategoryType>();
        }

        public IEnumerable<Language> GetLanguageList()
            => _languageService.GetSystemLanguages();

        public IEnumerable<CurrencyType> GetCurrencyList(bool isInternal)
        {
            var currencyList = _cacheService.Get<IEnumerable<CurrencyType>>(CacheKey.CurrencyList);
            if (currencyList == null)
            {
                currencyList = _marketingRepository.GetCurrencyList(isInternal);
            }

            if (currencyList != null)
            {
                _cacheService.Set(CacheKey.CurrencyList, currencyList);
            }

            return currencyList ?? new List<CurrencyType>();
        }

        public IEnumerable<SizeType> GetSizeList(SizeParam param, bool isInternal)
        {
            var languageList = _languageService.GetSystemLanguages();
            param.Languages = GetLanguageName(languageList, param.Language);

            return _marketingRepository.GetSizeList(param, isInternal);
        }

        public IEnumerable<BannerType> GetBannerTypeList(bool isInternal)
        {
            var bannerTypeList = _cacheService.Get<IEnumerable<BannerType>>(CacheKey.BannerTypeList);
            if (bannerTypeList == null)
            {
                bannerTypeList = _marketingRepository.GetBannerTypeList(isInternal);
            }

            if (bannerTypeList != null)
            {
                _cacheService.Set(CacheKey.BannerTypeList, bannerTypeList);
            }

            return bannerTypeList ?? new List<BannerType>();
        }

        public IEnumerable<Domain> GetDomainList(int affId, bool isInternal)
        {
            var domainList = _cacheService.Get<IEnumerable<Domain>>(CacheKey.DomainList);
            if (domainList == null)
            {
                domainList = _marketingRepository.GetDomainList(affId, isInternal);
            }

            if (domainList != null)
            {
                _cacheService.Set(CacheKey.DomainList, domainList, Number.Ten);
            }

            return domainList ?? new List<Domain>();
        }

        public IDictionary<string, string> GenerateLinkList(LinkGenerationParam linkGenerationParam)
        {
            var linkList = new Dictionary<string, string>();
            var selectedLanguage = linkGenerationParam.SelectedLanguage;
            var languageListFromParam = linkGenerationParam.LanguageList;
            var languageList = _languageService.GetSystemLanguages();

            if (IsSelectedAllLanguages(selectedLanguage) && languageListFromParam != null && languageListFromParam.Any())
            {
                foreach (var item in languageListFromParam)
                {
                    var language = languageList.FirstOrDefault(l => l.MarketId.MatchWith(item.Value));

                    if (language != null)
                    {
                        linkList.Add(language.MarketNativeName, GenerateLink(linkGenerationParam, language.ShortedCode));
                    }
                }

                var preferredLanguage = languageList.FirstOrDefault(l => l.MarketId.MatchWith(linkGenerationParam.PreferredLanguage));
                linkList.Add(Lang.Default, GenerateLink(linkGenerationParam, preferredLanguage?.ShortedCode ?? SystemLanguage.ShortedDefaultCode));
            }
            else
            {
                var language = languageList.FirstOrDefault(l => l.MarketId.MatchWith(selectedLanguage));
                if (language != null)
                {
                    linkList.Add(language.MarketNativeName, GenerateLink(linkGenerationParam, language.ShortedCode));
                }
                else
                {
                    linkList.Add(Lang.Default, GenerateLink(linkGenerationParam, SystemLanguage.ShortedDefaultCode));
                }
            }

            return linkList;
        }

        public IEnumerable<MarketingItem> GetBannerList(MarketingBannerParam bannerParam)
        {
            const int LengthOfSplittingSize = 2;

            var languageList = _languageService.GetSystemLanguages();
            bannerParam.Languages = GetLanguageName(languageList, bannerParam.Language);

            var bannerList = _marketingRepository.GetBannerList(bannerParam);

            foreach (var banner in bannerList)
            {
                banner.FileName = bannerParam.BannerDomain + banner.FileName;
                banner.SelectedDomainName = bannerParam.SelectedDomain.DomainName;

                var sizes = banner.Size.Split('x');
                if (sizes.Count() == LengthOfSplittingSize)
                {
                    banner.Width = Convert.ToInt32(sizes[0]);
                    banner.Height = Convert.ToInt32(sizes[1]);
                }

                var language = languageList.FirstOrDefault(l => l.MarketName.MatchWith(banner.Language));

                banner.RedirectLink = GenerateLink(bannerParam, language?.ShortedCode ?? SystemLanguage.ShortedDefaultCode);
            }

            return bannerList;
        }

        public IEnumerable<MarketingItem> GetTextList(MarketingTextParam textParam)
        {
            var languageList = _languageService.GetSystemLanguages();
            textParam.Languages = GetLanguageName(languageList, textParam.Language);

            var textList = _marketingRepository.GetTextList(textParam);

            foreach (var text in textList)
            {
                text.Message = HttpContext.Current.Server.HtmlDecode(text.Message.Replace("\r\n", "<br/>"));
                text.ScriptSrc = textParam.ScriptUrl + text.TextId;
                var language = languageList.FirstOrDefault(l => l.MarketName.MatchWith(text.Language));

                text.RedirectLink = GenerateLink(textParam, language?.ShortedCode ?? SystemLanguage.ShortedDefaultCode);
            }

            return textList;
        }

        public IEnumerable<LandingPage> GetLandingPageList(bool isInternal)
        {
            return _marketingRepository.GetLandingPageList(isInternal);
        }

        private static string GenerateLink(LinkBaseParam linkParam, string shortedLanguageCode)
        {
            var selectedDomain = linkParam.SelectedDomain;
            var template = linkParam.TextLinkTemplate;
            var scheme = selectedDomain.IsSsl ? Uri.UriSchemeHttps : Uri.UriSchemeHttp;

            var result = template.Replace("{scheme}", scheme)
                                .Replace("{domain}", selectedDomain.DomainName)
                                .Replace("{custid}", linkParam.AffId.ToString())
                                .Replace("{target}", HttpUtility.UrlPathEncode(linkParam.SelectedPage))
                                .Replace("{lang}", shortedLanguageCode);

            return result;
        }

        private static bool IsSelectedAllLanguages(string selectedLang)
            => string.IsNullOrWhiteSpace(selectedLang);

        public static string GetLanguageName(IEnumerable<Language> languageList, string languageCode)
        {
            var languageName = languageList.FirstOrDefault(l => l.MarketId.MatchWith(languageCode))?.MarketName;

            if(string.IsNullOrEmpty(languageName))
            {
                var allLanguageName = languageList.Select(l => l.MarketName).ToList();

                return string.Join(",", allLanguageName);
            }

            return languageName;
        }
    }
}