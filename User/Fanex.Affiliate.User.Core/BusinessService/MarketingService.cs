using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Fanex.Affiliate.User.Core.Constants;
using Fanex.Affiliate.User.Core.CoreService;
using Fanex.Affiliate.User.Core.DTO;
using Fanex.Affiliate.User.Core.DTO.System;
using Fanex.Affiliate.User.Core.DTO.UserPanel.AccountOverview;
using Fanex.Affiliate.User.Core.DTO.UserPanel.MediaGallery;
using Fanex.Affiliate.User.Core.Extensions;
using Fanex.Affiliate.User.Core.MetalModels.UserPanel;
using Fanex.Affiliate.User.Core.Repository.UserPanel;
using Fanex.Affiliate.User.Core.Resources;
using Fanex.Affiliate.User.Core.Utilities;

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

        public IEnumerable<Domain> GetDomainListByTargetCountry(string targetCountry, int affId, bool isInternal)
        {
            var domainList = _marketingRepository.GetDomainListByTargetCountry(targetCountry, isInternal);
            var specificDomains = (_appSetting.IsDev || _appSetting.IsLocal) ?
                                    SpecificDomain.SpecificDomainByAffOnLocal : SpecificDomain.SpecificDomainByAffOnPro;

            if (specificDomains.TryGetValue(affId.ToString(), out string domainName))
            {
                var specificDomainList = new List<Domain> { new Domain { DomainId = affId, DomainName = domainName, IsSsl = true } };
                specificDomainList.AddRange(domainList.Where(d => !d.DomainName.MatchWith(domainName)));

                return specificDomainList;
            }
            return domainList;
        }

        public IEnumerable<AffiliateLink> GenerateLinkList(LinkGenerationParam linkGenerationParam)
        {
            var linkList = new List<AffiliateLink>();
            var selectedLanguage = linkGenerationParam.SelectedLanguage;
            var languageListFromParam = linkGenerationParam.LanguageList;
            var languageList = _systemService.GetSystemTargetLanguagesByCountry(linkGenerationParam.SelectedTargetCountry);

            if (IsSelectedAllLanguages(selectedLanguage) && languageListFromParam != null && languageListFromParam.Any())
            {
                foreach (var item in languageListFromParam)
                {
                    AddGenerateLanguageLinkToList(languageList, linkList, linkGenerationParam, item.Value);
                }

                AddGenerateDefaultLinkToList(languageList, linkList, linkGenerationParam);
            }
            else
            {
                var isValidLanguage = AddGenerateLanguageLinkToList(languageList, linkList, linkGenerationParam, selectedLanguage);

                if (!isValidLanguage)
                {
                    AddGenerateDefaultLinkToList(languageList, linkList, linkGenerationParam);
                }
            }

            return linkList;
        }

        private void AddGenerateDefaultLinkToList(IEnumerable<TargetLanguage> languageList, List<AffiliateLink> linkList, LinkGenerationParam linkGenerationParam)
        {
            linkList.Add(new AffiliateLink
            {
                Language = Lang.Default,
                Link = GenerateDefaultLinkByTargetCountry(languageList, linkGenerationParam),
                LanguageCode = SystemLanguage.DefaultLanguageCode
            });
        }

        private static bool AddGenerateLanguageLinkToList(IEnumerable<TargetLanguage> languageList, List<AffiliateLink> linkList, LinkGenerationParam linkGenerationParam, string languageCode)
        {
            var language = languageList.FirstOrDefault(l => l.LanguageCode.MatchWith(languageCode));
            var isValidLanguage = language != null;

            if (isValidLanguage)
            {
                linkList.Add(new AffiliateLink
                {
                    Language = language.LanguageName,
                    Link = GenerateLink(linkGenerationParam, language.ShortedCode),
                    LanguageCode = language.LanguageCode
                });
            }

            return isValidLanguage;
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

        public IEnumerable<LandingPage> GetLandingPageList(string targetCountry, string commGroupName, bool isInternal)
        {
            var landingPages = _marketingRepository.GetLandingPageListByTargetCountry(targetCountry ?? string.Empty, isInternal);

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