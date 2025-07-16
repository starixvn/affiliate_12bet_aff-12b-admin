namespace Fanex.Affiliate.User.Core.BusinessService
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using Commands.Marketing;
    using Constants;
    using CoreService;
    using Criterias;
    using Data.Repository;
    using DTO;
    using DTO.UserPanel;
    using DTO.UserPanel.MediaGallery;
    using Enum;
    using Extensions;
    using MetalModels;
    using MetalModels.UserPanel;
    using Resources;
    using Utilities;
    using ViewModels.UserPanel.Marketing;

    public class MarketingService : BaseDynamicService, IMarketingService
    {
        private readonly IMemoryCacheService _cacheService;
        private readonly IAppSetting _appSetting;
        private readonly ISystemService _systemsService;

        private const int AffiliateLink = 1;
        private const int MediaGallery = 2;

        public MarketingService(
            IDynamicRepository dynamicRepository,
            IMemoryCacheService cacheService,
            IAppSetting appSetting,
            ISystemService systemsService) : base(dynamicRepository)
        {
            _cacheService = cacheService;
            _appSetting = appSetting;
            _systemsService = systemsService;
        }

        public void TrackingAffMarketingLink(int affId, IDictionary<string, QrCodeLinkModel> qrCodeLinks, QrCodeParam param)
        {
            var affMediaLinks = GetAffMarketingLinks(affId, param.SelectedChannel);
            var shouldTrackingAffLinks = qrCodeLinks.Values;

            if (affMediaLinks != null)
            {
                shouldTrackingAffLinks = new List<QrCodeLinkModel>();

                foreach (var item in qrCodeLinks.Values)
                {
                    var affLink = affMediaLinks.FirstOrDefault(l => l.AffMediaLink.Equals(item.Link, StringComparison.OrdinalIgnoreCase));

                    if (affLink is null)
                    {
                        item.IsShow = true;
                        shouldTrackingAffLinks.Add(item);
                    }
                    else
                    {
                        item.IsShow = affLink.IsShow;
                    }
                }
            }

            foreach (var item in shouldTrackingAffLinks)
            {
                var trackingParam = new MarketingLinkParam
                {
                    AffId = affId,
                    AffMediaLink = item.Link,
                    ChannelCode = param.SelectedChannel,
                    LanguageCode = item.Language.Code,
                    IsOn = item.IsShow
                };

                ToggleMarketingLink(trackingParam);
            }
        }

        public IEnumerable<CategoryType> GetCategoryList(bool isMediaPage, AffiliateLinkType linkType, bool isInternal = false)
        {
            var mediaType = isMediaPage ? AffiliateLink : MediaGallery;
            var param = new MediaCategoryParam
            {
                TargetId = (int)linkType,
                TypeId = mediaType
            };
            var categoryList = FetchTData<CategoryType>(new GetCategoryListCriteria(param.TypeId, param.TargetId, isInternal));

            return categoryList ?? new List<CategoryType>();
        }

        public IEnumerable<CurrencyType> GetCurrencyList(bool isInternal)
        {
            var currencyList = _cacheService.Get<IEnumerable<CurrencyType>>(CacheKey.CurrencyList);

            if (currencyList == null)
            {
                currencyList = FetchTData<CurrencyType>(new GetCurrencyListCriteria(isInternal));
                _cacheService.Set(CacheKey.CurrencyList, currencyList);
            }

            return currencyList ?? new List<CurrencyType>();
        }

        public IEnumerable<SizeType> GetSizeList(SizeParam param, bool isInternal = false)
        {
            param.Language = ConvertSelectedLanguageNames(param.TargetCountryCode, param.Language);

            var criteria = param.MapTo<SizeParam, GetSizeListCriteria>(isInternal);

            return FetchTData<SizeType>(criteria);
        }

        public IEnumerable<Domain> GetAvailableDomainList(AvailableDomainParam param)
        {
            var isMember = param.LinkType == AffiliateLinkType.AffiliateLink;

            var domainList = FetchAvailableDomains(param, isMember);

            return domainList ?? Enumerable.Empty<Domain>();
        }

        private IEnumerable<Domain> FetchAvailableDomains(AvailableDomainParam param, bool isMember)
        {
            if (isMember)
            {
                var domainList = FetchTData<MappingDomainInfo>(new GetAffDomainListCriteria(param.AffId, param.TargetCountryCode, param.IsInternal));

                return StatisticsHelper.GetCustomAvailableDomainList(_appSetting, domainList, param.AffId, param.CommGroupName);
            }    

            return FetchTData<Domain>(new GetSubAffDomainListCriteria(param.IsInternal));
        }

        public IDictionary<string, LanguageLinkViewModel> GenerateLinkList(LinkGeneratingParam linkGeneratedParam)
        {
            var languages = GetLanguageUsedForGeneration(linkGeneratedParam);
            return GenerateLink(languages, linkGeneratedParam);

        }

        public IEnumerable<MarketingItem> GetBannerList(MarketingBannerParam bannerParam)
        {
            const int LengthOfSplittingSize = 2;
            bannerParam.Language = ConvertSelectedLanguageNames(bannerParam.SelectedTargetCountry, bannerParam.Language);

            var bannerList = FetchTDataWithPaging<MarketingItem>(bannerParam.MapTo<MarketingBannerParam, GetBannerListCriteria>(), bannerParam);
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

                var language = _systemsService.GetLanguageByTargetCountry(bannerParam.SelectedTargetCountry).FirstOrDefault(l => l.EnglishName.MatchWith(banner.Language));

                if (language != null)
                {
                    banner.RedirectLink = GenerateLink(bannerParam, language.ShortedCode);
                }
            }

            return bannerList;
        }

        public IEnumerable<LandingPage> GetLandingPageList(LandingPageParam param)
        {
            var isTargetToMember = param.LinkType == AffiliateLinkType.AffiliateLink;
            var landingPages = FetchTData<LandingPage>(new GetLandingPageListCriteria(param.IsInternal, param.TargetCountryCode ?? string.Empty));

            return isTargetToMember
                ? StatisticsHelper.GetFilteredLandingPageList(_appSetting, landingPages, param.IsSelectedMobileDomain, param.IsUsingInStatistics)
                : GetSubAffLandingPages();
        }

        public string GetTextLinkTemplate(AffiliateLinkType linkType)
        {
            switch (linkType)
            {
                case AffiliateLinkType.AffiliateLink:
                    return _appSetting.TextLinkTemplate;
                case AffiliateLinkType.SubAffiliateLink:
                    return _appSetting.SubAffTextLinkTemplate;
                default:
                    return _appSetting.TextLinkTemplate;
            }
        }

        public ResultInfo<MarketingChannel> AddMarketingChannel(int userId, string channelName)
        {
            var channelCode = NormalizeName(channelName);
            var result = ExecuteReturnResultInfo(new AddMarketingChannelCommand(userId, channelCode, channelName));
            var message = GenAddChannelErrMsg(result.Code);

            return new ResultInfo<MarketingChannel>
            {
                IsOk = result.IsOk,
                Message = string.IsNullOrWhiteSpace(message) ? result.Message : message,
                Data = new MarketingChannel { ChannelCode = channelCode, ChannelName = channelName }
            };
        }

        public IDictionary<string, string> GetChannels(int userId)
            => FetchTData<MarketingChannel>(new GetMarketingChannelsCriteria(userId))
                .ToDictionary(c => c.ChannelCode, c => c.ChannelName);

        public ResultInfo ToggleMarketingLink(MarketingLinkParam param)
        {
            var result = ExecuteReturnResultInfo(param.MapTo<MarketingLinkParam, ToggleMarketingLinkCommand>());
            var message = GenToggleErrMsg(result.Code);

            return new ResultInfo
            {
                IsOk = result.IsOk,
                Message = string.IsNullOrWhiteSpace(message) ? result.Message : message
            };
        }

        private IEnumerable<MarketingChannelLink> GetAffMarketingLinks(int userId, string channelCode)
            => FetchTData<MarketingChannelLink>(new GetAffMarketingLinkCriteria(userId, channelCode));

        public IEnumerable<IGrouping<string, MarketingChannelLink>> GetUsingMarketingLinks(int userId)
            => FetchTData<MarketingChannelLink>(new GetUsingMarketingLinkCriteria(userId)).GroupBy(c => c.ChannelCode);

        private string ConvertSelectedLanguageNames(string targetCountryCode, string languageCode)
        {
            var languageList = _systemsService.GetLanguageByTargetCountry(targetCountryCode);
            if (string.IsNullOrEmpty(languageCode))
            {
                var allLanguageName = languageList.Select(l => l.EnglishName).ToList();
                return string.Join(",", allLanguageName);
            }

            return languageList.FirstOrDefault(l => l.Code.MatchWith(languageCode))?.EnglishName;
        }

        private Dictionary<string, Language> GetLanguageUsedForGeneration(LinkGeneratingParam linkGeneratedParam)
        {
            var result = new Dictionary<string, Language>();
            var languageList = _systemsService.GetLanguageByTargetCountry(linkGeneratedParam.SelectedTargetCountry);

            if (string.IsNullOrEmpty(linkGeneratedParam.SelectedLanguage))
            {
                foreach (var language in languageList)
                {
                    result.Add(language.Code, language);
                }
            }
            else
            {
                var language = languageList.FirstOrDefault(l => l.Code.MatchWith(linkGeneratedParam.SelectedLanguage));
                if (language != null)
                {
                    result.Add(language.Code, language);
                }
            }

            return result;
        }

        private static IDictionary<string, LanguageLinkViewModel> GenerateLink(Dictionary<string, Language> languages, LinkGeneratingParam param)
        {
            var linkDics = new Dictionary<string, LanguageLinkViewModel>();

            foreach (var lang in languages.Values)
            {
                var linkViewModel = new LanguageLinkViewModel
                {
                    Language = lang,
                    Link = GenerateLink(param, lang.ShortedCode)
                };

                linkDics.Add(lang.NativeName, linkViewModel);
            }

            return linkDics;
        }

        private static string GenerateLink(LinkBaseParam linkParam, string shortedLanguageCode)
        {
            var selectedDomain = linkParam.SelectedDomain;
            var template = linkParam.TextLinkTemplate;
            var scheme = selectedDomain.IsSsl ? Uri.UriSchemeHttps : Uri.UriSchemeHttp;

            var result = template.Replace("{scheme}", scheme)
                .Replace("{domain}", selectedDomain.DomainName)
                .Replace("{affCode}", linkParam.AffCode.ToString())
                .Replace("{target}", HttpUtility.UrlPathEncode(linkParam.SelectedPage))
                .Replace("{lang}", shortedLanguageCode)
                .Replace("{channel}", linkParam.SelectedChannel);

            return result;
        }

        private static IEnumerable<LandingPage> GetSubAffLandingPages()
        {
            return new List<LandingPage>
            {
                new LandingPage{PageCode = "Signup", PageName="SignUp"}
            };
        }

        private static string GenAddChannelErrMsg(int code)
        {
            switch (code)
            {
                case ErrCodes.OneHundredOne:
                    return Error.ErrInValidAff;
                case ErrCodes.OneHundredTwo:
                    return Error.ErrChannelExisted;
                case ErrCodes.OneHundredThree:
                    return Error.ErrInternalError;
                default:
                    return string.Empty;
            }
        }

        private static string GenToggleErrMsg(int code)
        {
            switch (code)
            {
                case ErrCodes.OneHundredOne:
                    return Error.ErrInValidAff;
                case ErrCodes.OneHundredTwo:
                    return Error.ErrInternalError;
                default:
                    return string.Empty;
            }
        }

        private static string NormalizeName(string channelName)
        {
            return channelName.ToLowerInvariant();
        }
    }
}