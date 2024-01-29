using Fanex.Affiliate.User.Core.Constants;
using Fanex.Affiliate.User.Core.CoreService;
using Fanex.Affiliate.User.Core.DTO;
using Fanex.Affiliate.User.Core.DTO.System;
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
        private readonly ISystemService _systemService;
        private readonly IAppSetting _appSetting;
        private readonly IAccountRepository _accountRepository;

        public MarketingService(
            IMarketingRepository marketingRepository,
            IMemoryCacheService cacheService,
            ISystemService systemService,
            IAppSetting appSetting,
            IAccountRepository accountRepository)
        {
            _marketingRepository = marketingRepository;
            _cacheService = cacheService;
            _systemService = systemService;
            _appSetting = appSetting;
            _accountRepository = accountRepository;
        }

        public string GetTargetCountryCodeByAff(int affId)
        {
            var affInfo = _accountRepository.GetAccountInfo(affId);

            return affInfo?.TargetCountryCode;
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

        public IEnumerable<SizeType> GetSizeList(SizeParam param, bool isInternal)
        {
            var languageList = _systemService.GetSystemTargetLanguagesByCountry(param.TargetCountry);
            param.Languages = GetTargetLanguageName(languageList, param.Language);

            return _marketingRepository.GetSizeList(param, isInternal);
        }

        public IEnumerable<Domain> GetDomainListByTargetCountry(string targetCountry, bool isInternal)
        {
            return _marketingRepository.GetDomainListByTargetCountry(targetCountry, isInternal)?.Where(d => !d.DomainName.Contains("glg46.com"));
        }

        public IDictionary<string, string> GenerateLinkList(LinkGenerationParam linkGenerationParam)
        {
            var linkList = new Dictionary<string, string>();
            var selectedLanguage = linkGenerationParam.SelectedLanguage;
            var languageListFromParam = linkGenerationParam.LanguageList;
            var languageList = _systemService.GetSystemTargetLanguagesByCountry(linkGenerationParam.SelectedTargetCountry);

            if (IsSelectedAllLanguages(selectedLanguage) && languageListFromParam != null && languageListFromParam.Any())
            {
                if (_appSetting.ODIWorldCupLandingPages.Contains(linkGenerationParam.SelectedPage))
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

                linkList.Add(Lang.Default, GenerateDefaultLinkByTargetCountry(languageList, linkGenerationParam));
            }
            else
            {
                var language = languageList.FirstOrDefault(l => l.LanguageCode.MatchWith(selectedLanguage));
                if (language != null)
                {
                    linkList.Add(language.LanguageName, GenerateLink(linkGenerationParam, language.ShortedCode));
                }
                else
                {
                    linkList.Add(Lang.Default, GenerateDefaultLinkByTargetCountry(languageList, linkGenerationParam));
                }
            }

            return linkList;
        }

        private static void AddGenerateLinkToList(IEnumerable<TargetLanguage> languageList, Dictionary<string, string> linkList, LinkGenerationParam linkGenerationParam, string languageCode)
        {
            var language = languageList.FirstOrDefault(l => l.LanguageCode.MatchWith(languageCode));

            if (language != null)
            {
                linkList.Add(language.LanguageName, GenerateLink(linkGenerationParam, language.ShortedCode));
            }
        }

        public IEnumerable<Banner> GetBannerList(MarketingBannerParam bannerParam)
        {
            const int LengthOfSplittingSize = 2;

            var languageList = _systemService.GetSystemTargetLanguagesByCountry(bannerParam.SelectedTargetCountry);
            bannerParam.Languages = GetTargetLanguageName(languageList, bannerParam.Language);

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

                var language = languageList.FirstOrDefault(l => l.LanguageName.MatchWith(banner.Language));

                banner.RedirectLink = GenerateLink(bannerParam, language?.ShortedCode ?? SystemLanguage.ShortedDefaultCode);
            }

            return bannerList;
        }

        public IEnumerable<LandingPage> GetSpecialLandingPageList(string targetCountry, string commGroupName, string language, bool isInternal)
        {
            var landingPages = GetLandingPageList(targetCountry, commGroupName, isInternal);

            if (string.IsNullOrEmpty(language) || _appSetting.SupportedODIWorldCupLanguages.Contains(language))
            {
                return landingPages;
            }

            return landingPages.Where(l => !_appSetting.ODIWorldCupLandingPages.Any(m => m.MatchWith(l.PageCode)));
        }

        public IEnumerable<LandingPage> GetLandingPageList(string targetCountry, string commGroupName, bool isInternal)
        {
            var landingPages = _marketingRepository.GetLandingPageListByTargetCountry(targetCountry, isInternal);

            return StatisticsHelper.GetFilteredLandingPageList(_appSetting, landingPages, commGroupName);
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
                                .Replace("{lang}", shortedLanguageCode)
                                .Replace("{country}", linkParam.SelectedTargetCountry.ToLowerInvariant());

            return result;
        }

        private static bool IsSelectedAllLanguages(string selectedLang)
            => string.IsNullOrWhiteSpace(selectedLang);

        private static string GetTargetLanguageName(IEnumerable<TargetLanguage> languageList, string languageCode)
        {
            var languageName = languageList.FirstOrDefault(l => l.LanguageCode.MatchWith(languageCode))?.LanguageName;

            if (string.IsNullOrEmpty(languageName))
            {
                var allLanguageName = languageList.Select(l => l.LanguageName).ToList();

                return string.Join(",", allLanguageName);
            }

            return languageName;
        }

        private string GenerateDefaultLinkByTargetCountry(IEnumerable<TargetLanguage> languageList, LinkGenerationParam linkGenerationParam)
        {
            var targetCountryList = _systemService.GetSystemTargetCountries();
            var selectedTargetCountry = targetCountryList.FirstOrDefault(t => t.TargetCountryCode.MatchWith(linkGenerationParam.SelectedTargetCountry));
            var defaultLanguage = languageList.FirstOrDefault(l => l.LanguageCode.MatchWith(selectedTargetCountry.DefaultLanguageCode));

            return GenerateLink(linkGenerationParam, defaultLanguage?.ShortedCode ?? SystemLanguage.ShortedDefaultCode);
        }
    }
}