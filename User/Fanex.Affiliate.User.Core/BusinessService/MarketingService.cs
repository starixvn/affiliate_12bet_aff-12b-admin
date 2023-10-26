using Fanex.Affiliate.User.Core.Constants;
using Fanex.Affiliate.User.Core.CoreService;
using Fanex.Affiliate.User.Core.DTO;
using Fanex.Affiliate.User.Core.DTO.UserPanel.MediaGallery;
using Fanex.Affiliate.User.Core.Extensions;
using Fanex.Affiliate.User.Core.MetalModels.UserPanel;
using Fanex.Affiliate.User.Core.Repository.UserPanel;
using Fanex.Affiliate.User.Core.Resources;
using Fanex.Affiliate.User.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fanex.Affiliate.User.Core.BusinessService
{
    public class MarketingService : IMarketingService
    {
        private readonly IMarketingRepository _marketingRepository;

        private readonly IMemoryCacheService _cacheService;
        private readonly ILanguageService _languageService;
        private readonly IAppSetting _appSetting;

        public MarketingService(
            IMarketingRepository marketingRepository,
            IMemoryCacheService cacheService,
            ILanguageService languageService,
            IAppSetting appSetting)
        {
            _marketingRepository = marketingRepository;
            _cacheService = cacheService;
            _languageService = languageService;
            _appSetting = appSetting;
        }

        public IEnumerable<TargetProduct> GetTargetProductList(bool isInternal)
        {
            var targetProductList = _cacheService.Get<IEnumerable<TargetProduct>>(CacheKey.TargetProductList);
            if (targetProductList == null)
            {
                targetProductList = _marketingRepository.GetTargetProductList(isInternal);

                _cacheService.Set(CacheKey.TargetProductList, targetProductList);
            }

            return targetProductList ?? new List<TargetProduct>();
        }

        public IEnumerable<Language> GetLanguageList()
            => _languageService.GetSystemLanguages();

        public IEnumerable<SizeType> GetSizeList(SizeParam param, bool isInternal)
        {
            var languageList = _languageService.GetSystemLanguages();
            param.Languages = GetLanguageName(languageList, param.Language);

            return _marketingRepository.GetSizeList(param, isInternal);
        }

        public IEnumerable<Domain> GetDomainList(int affId, bool isInternal)
        {
            var domainList = _cacheService.Get<IEnumerable<Domain>>(CacheKey.DomainList);

            if (domainList == null)
            {
                domainList = _marketingRepository.GetDomainList(affId, isInternal)?.Where(d => !d.DomainName.Contains("glg46.com"));
                _cacheService.Set(CacheKey.DomainList, domainList);
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
                if(_appSetting.ODIWorldCupLandingPages.Contains(linkGenerationParam.SelectedPage))
                {
                    foreach (var item in _appSetting.SupportedODIWorldCupLanguages)
                    {
                        AddGenerateLinkToList(languageList, linkList, linkGenerationParam, item);
                    }

                    return linkList;
                }

                foreach (var item in languageListFromParam)
                {
                    AddGenerateLinkToList(languageList, linkList, linkGenerationParam, item.Value);
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

        private static void AddGenerateLinkToList(IEnumerable<Language> languageList, Dictionary<string, string> linkList, LinkGenerationParam linkGenerationParam, string marketId)
        {
            var language = languageList.FirstOrDefault(l => l.MarketId.MatchWith(marketId));

            if (language != null)
            {
                linkList.Add(language.MarketNativeName, GenerateLink(linkGenerationParam, language.ShortedCode));
            }
        }

        public IEnumerable<Banner> GetBannerList(MarketingBannerParam bannerParam)
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

        public IEnumerable<LandingPage> GetSpecialLandingPageList(string commGroupName, string targetCountry, string language, bool isInternal)
        {
            var landingPages = GetLandingPageList(commGroupName, isInternal);

            if (string.IsNullOrEmpty(language) || _appSetting.SupportedODIWorldCupLanguages.Contains(language))
            {
                return landingPages;
            }

            return landingPages.Where(l => !_appSetting.ODIWorldCupLandingPages.Any(m => string.Equals(l.PageCode, m, StringComparison.OrdinalIgnoreCase)));
        }

        public IEnumerable<LandingPage> GetLandingPageList(string commGroupName, bool isInternal)
        {
            var landingPages = GetCachedLandingPageList(isInternal);

            if (_appSetting.MarketingForCommGroups.Any(c => string.Equals(c, commGroupName, StringComparison.OrdinalIgnoreCase)))
            {
                return landingPages;
            }

            return landingPages.Where(l => !_appSetting.MarketingLandingPages.Any(m => string.Equals(l.PageCode, m, StringComparison.OrdinalIgnoreCase)));
        }

        public IEnumerable<LandingPage> GetCachedLandingPageList(bool isInternal)
        {
            var landingPages = _cacheService.Get<IEnumerable<LandingPage>>(CacheKey.LandingPageList);

            if (landingPages == null)
            {
                var landingPageList = _marketingRepository.GetLandingPageList(isInternal);

                landingPages = GetSortedLandingPageList(landingPageList);
                _cacheService.Set(CacheKey.LandingPageList, landingPages);
            }

            return landingPages ?? new List<LandingPage>();
        }

        public IEnumerable<LandingPage> GetSortedLandingPageList(IEnumerable<LandingPage> landingPages)
        {
            var excludedODIlangdingPages = landingPages.Where(l => !_appSetting.ODIWorldCupLandingPages.Any(m => string.Equals(l.PageCode, m, StringComparison.OrdinalIgnoreCase)));
            var specialLandingPages = landingPages.Where(l => _appSetting.ODIWorldCupLandingPages.Any(m => string.Equals(l.PageCode, m, StringComparison.OrdinalIgnoreCase)));
            return excludedODIlangdingPages.Concat(specialLandingPages);
        }

        public ResultInfo CountNumberOfCopies(CopiedBannerCounterParam param)
        {
            return _marketingRepository.CountNumberOfCopies(param);
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

        private static string GetLanguageName(IEnumerable<Language> languageList, string languageCode)
        {
            var languageName = languageList.FirstOrDefault(l => l.MarketId.MatchWith(languageCode))?.MarketName;

            if (string.IsNullOrEmpty(languageName))
            {
                var allLanguageName = languageList.Select(l => l.MarketName).ToList();

                return string.Join(",", allLanguageName);
            }

            return languageName;
        }
    }
}