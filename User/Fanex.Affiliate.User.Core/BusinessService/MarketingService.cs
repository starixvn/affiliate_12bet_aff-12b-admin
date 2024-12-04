using Fanex.Affiliate.User.Core.Constants;
using Fanex.Affiliate.User.Core.CoreService;
using Fanex.Affiliate.User.Core.DTO;
using Fanex.Affiliate.User.Core.DTO.UserPanel;
using Fanex.Affiliate.User.Core.DTO.UserPanel.MediaGallery;
using Fanex.Affiliate.User.Core.Enum;
using Fanex.Affiliate.User.Core.Extensions;
using Fanex.Affiliate.User.Core.MetalModels;
using Fanex.Affiliate.User.Core.MetalModels.UserPanel;
using Fanex.Affiliate.User.Core.Repository.UserPanel;
using Fanex.Affiliate.User.Core.Resources;
using Fanex.Affiliate.User.Core.Utilities;
using Fanex.Affiliate.User.Core.ViewModels.UserPanel.Marketing;
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
        private readonly IAppSetting _appSetting;
        private readonly ISystemService _languageService;

        private const int AffiliateLink = 1;
        private const int MediaGallery = 2;

        public MarketingService(IMarketingRepository marketingRepository, IMemoryCacheService cacheService, IAppSetting appSetting, ISystemService languageService)
        {
            _marketingRepository = marketingRepository;
            _cacheService = cacheService;
            _appSetting = appSetting;
            _languageService = languageService;
        }

        public IEnumerable<CategoryType> GetCategoryList(bool isMediaPage, AffiliateLinkType linkType, bool isInternal = false)
        {
            var cacheKey = isMediaPage ? CacheKey.BannerCategoryList : CacheKey.TextCategoryList;
            cacheKey = $"{cacheKey}-{linkType}";

            var categoryList = _cacheService.Get<IEnumerable<CategoryType>>(cacheKey);
            if (categoryList == null)
            {
                var mediaType = isMediaPage ? AffiliateLink : MediaGallery;
                var param = new MediaCategoryParam
                {
                    TargetId = (int)linkType,
                    TypeId = mediaType
                };
                categoryList = _marketingRepository.GetCategoryList(param, isInternal);

                _cacheService.Set(cacheKey, categoryList);
            }

            return categoryList ?? new List<CategoryType>();
        }

        public IEnumerable<LanguageType> GetLanguageList(bool isInternal)
            => _languageService.GetLanguageList(isInternal)
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
                _cacheService.Set(CacheKey.CurrencyList, currencyList);
            }

            return currencyList ?? new List<CurrencyType>();
        }

        public IEnumerable<SizeType> GetSizeList(SizeParam param, bool isInternal = false)
        {
            return _marketingRepository.GetSizeList(param, isInternal);
        }

        public IEnumerable<BannerType> GetBannerTypeList(bool isInternal = false)
        {
            var bannerTypeList = _cacheService.Get<IEnumerable<BannerType>>(CacheKey.BannerTypeList);

            if (bannerTypeList == null)
            {
                bannerTypeList = _marketingRepository.GetBannerTypeList(isInternal);
                _cacheService.Set(CacheKey.BannerTypeList, bannerTypeList);
            }

            return bannerTypeList ?? new List<BannerType>();
        }

        public IEnumerable<Domain> GetAvailableDomainList(int affId, AffiliateLinkType linkType, bool isInternal = false)
        {
            var isMember = linkType == AffiliateLinkType.AffiliateLink;
            var cacheKey = isMember ? CacheKey.DomainList : CacheKey.SubDomainList;

            var domainList = _cacheService.Get<IEnumerable<Domain>>(cacheKey);

            if (domainList == null)
            {
                domainList = _marketingRepository.GetAvailableDomainList(affId, isMember, isInternal);
                var specificDomains = _appSetting.IsDev || _appSetting.IsLocal
                    ? SpecificDomain.SpecificDomainByAffOnLocal
                    : SpecificDomain.SpecificDomainByAffOnPro;

                if (specificDomains.TryGetValue(affId.ToString(), out var domainNames))
                {
                    var names = domainNames.Split(',');
                    var specificDomainList = new List<Domain>();

                    foreach (var name in names)
                    {
                        specificDomainList.Add(new Domain
                        {
                            DomainId = affId + specificDomainList.Count,
                            DomainName = name,
                            IsSsl = false
                        });
                    }

                    specificDomainList.AddRange(domainList.Where(d => !Array.Exists(names, n => n.MatchWith(d.DomainName))));
                    _cacheService.Set(cacheKey, specificDomainList, Number.Ten);

                    return specificDomainList;
                }

                _cacheService.Set(cacheKey, domainList, Number.Ten);
            }

            return domainList ?? new List<Domain>();
        }

        public IDictionary<string, LanguageLinkViewModel> GenerateLinkList(LinkGeneratingParam linkGeneratedParam)
        {
            var languages = GetLanguageUsedForGeneration(linkGeneratedParam);
            return GenerateLink(languages, linkGeneratedParam);

        }

        public IEnumerable<MarketingItem> GetBannerList(MarketingBannerParam bannerParam)
        {
            const int lengthOfSplittingSize = 2;
            var bannerList = _marketingRepository.GetBannerList(bannerParam);

            foreach (var banner in bannerList)
            {
                banner.FileName = bannerParam.BannerDomain + banner.FileName;
                banner.SelectedDomainName = bannerParam.SelectedDomain.DomainName;

                var sizes = banner.Size.Split('x');
                if (sizes.Count() == lengthOfSplittingSize)
                {
                    banner.Width = Convert.ToInt32(sizes[0]);
                    banner.Height = Convert.ToInt32(sizes[1]);
                }

                var language = _languageService.GetLanguageList().FirstOrDefault(l => l.EnglishName.MatchWith(banner.Language));

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
                text.Message = HttpContext.Current.Server.HtmlDecode(text.Message.Replace("\r\n", "<br/>").Replace("\n", "<br/>"));
                text.ScriptSrc = $"{textParam.ScriptUrl}?textId={text.TextId}&targetId={textParam.TargetId}";
                var language = _languageService.GetLanguageList().FirstOrDefault(l => l.EnglishName.MatchWith(text.Language));

                if (language != null)
                {
                    text.RedirectLink = GenerateLink(textParam, language.ShortedCode);
                }
            }

            return textList;
        }

        public IEnumerable<LandingPage> GetLandingPageList(string commGroupName, bool isInternal = false, AffiliateLinkType linkType = AffiliateLinkType.AffiliateLink)
        {
            var isTargetToMember = linkType == AffiliateLinkType.AffiliateLink;
            var landingPages = _marketingRepository.GetLandingPageList(isInternal);

            return isTargetToMember
                ? StatisticsHelper.GetFilteredLandingPageList(_appSetting, landingPages, commGroupName)
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
            var result = _marketingRepository.AddMarketingChannel(userId, channelCode, channelName);
            var message = GenAddChannelErrMsg(result.Code);

            return new ResultInfo<MarketingChannel>
            {
                IsOk = result.IsOk,
                Message = string.IsNullOrWhiteSpace(message) ? result.Message : message,
                Data = new MarketingChannel { ChannelCode = channelCode, ChannelName = channelName }
            };
        }

        public IDictionary<string, string> GetChannels(int userId)
            => _marketingRepository.GetMarketingChannels(userId).ToDictionary(c => c.ChannelCode, c => c.ChannelName);

        public ResultInfo ToggleMarketingLink(MarketingLinkParam param)
        {
            var result = _marketingRepository.ToggleMarketingLink(param);
            var message = GenToggleErrMsg(result.Code);

            return new ResultInfo
            {
                IsOk = result.IsOk,
                Message = string.IsNullOrWhiteSpace(message) ? result.Message : message
            };
        }

        public IEnumerable<IGrouping<string, MarketingChannelLink>> GetUsingMarketingLinks(int userId)
            => _marketingRepository.GetUsingMarketingLink(userId).GroupBy(c => c.ChannelCode);

        private Dictionary<string, Language> GetLanguageUsedForGeneration(LinkGeneratingParam linkGeneratedParam)
        {
            var result = new Dictionary<string, Language>();
            var languageList = _languageService.GetLanguageList();

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

            foreach (var item in languages)
            {
                var lang = item.Value;
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
                                .Replace("{custid}", linkParam.AffCode.ToString())
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
            return channelName.Replace(" ", string.Empty).ToLowerInvariant();
        }
    }
}