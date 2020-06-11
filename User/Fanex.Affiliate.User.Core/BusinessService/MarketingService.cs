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

        public IEnumerable<LanguageType> GetLanguageList(bool isInternal)
            => _languageService.GetSystemLanguages(isInternal)
            .Select(l => new LanguageType
            {
                DisplayName = l.DisplayName,
                MarketId = l.Code,
                MarketName = l.EnglishName,
                MarketNativeName = l.NativeName,
                ShortedCode = l.ShortedCode
            });

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
            if (string.IsNullOrEmpty(selectedLanguage))
            {
                return linkList;
            }

            var languageList = linkGenerationParam.LanguageList;
            if (languageList != null && languageList.Any())
            {
                foreach (var item in languageList)
                {
                    var language = _languageService.GetSystemLanguages().FirstOrDefault(l => l.Code.MatchWith(item.Value));

                    if (language != null)
                    {
                        linkList.Add(language.NativeName, GenerateLink(linkGenerationParam, language.ShortedCode));
                    }
                }

                var preferredLanguage = _languageService.GetSystemLanguages().FirstOrDefault(l => l.Code.MatchWith(linkGenerationParam.PreferredLanguage));
                linkList.Add(Lang.Default, GenerateLink(linkGenerationParam, preferredLanguage.ShortedCode));
            }
            else
            {
                var language = _languageService.GetSystemLanguages().FirstOrDefault(l => l.Code.MatchWith(selectedLanguage));

                if (language != null)
                {
                    linkList.Add(language.NativeName, GenerateLink(linkGenerationParam, language.ShortedCode));
                }
            }

            return linkList;
        }

        public IEnumerable<MarketingItem> GetBannerList(MarketingBannerParam bannerParam)
        {
            const int LengthOfSplittingSize = 2;
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

                var language = _languageService.GetSystemLanguages().FirstOrDefault(l => l.EnglishName.MatchWith(banner.Language));

                if (language != null)
                {
                    banner.RedirectLink = GenerateLink(bannerParam, language.ShortedCode);
                }

            }

            return bannerList;
        }

        public IEnumerable<MarketingItem> GetTextList(MarketingTextParam textParam)
        {
            var textList = _marketingRepository.GetTextList(textParam);

            foreach (var text in textList)
            {
                text.Message = HttpContext.Current.Server.HtmlDecode(text.Message.Replace("\r\n", "<br/>"));
                text.ScriptSrc = textParam.ScriptUrl + text.TextId;
                var language = _languageService.GetSystemLanguages().FirstOrDefault(l => l.EnglishName.MatchWith(text.Language));

                if (language != null)
                {
                    text.RedirectLink = GenerateLink(textParam, language.ShortedCode);
                }

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
    }
}