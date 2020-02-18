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

namespace Fanex.Affiliate.User.Core.BusinessService
{
    public class OldMarketingService : IOldMarketingService
    {
        private readonly IOldMarketingRepository _marketingRepository;

        private readonly IMemoryCacheService _cacheService;

        public OldMarketingService(IOldMarketingRepository marketingRepository, IMemoryCacheService cacheService)
        {
            _marketingRepository = marketingRepository;
            _cacheService = cacheService;
        }

        public IEnumerable<OldBannerType> GetBannerTypeList(bool isInternal)
        {
            var bannerTypeList = _cacheService.Get<IEnumerable<OldBannerType>>(CacheKey.OldBannerTypeList);
            if (bannerTypeList == null)
            {
                bannerTypeList = _marketingRepository.GetBannerTypeList(isInternal);
            }

            if (bannerTypeList != null)
            {
                _cacheService.Set(CacheKey.OldBannerTypeList, bannerTypeList);
            }

            return bannerTypeList ?? new List<OldBannerType>();
        }

        public IEnumerable<Domain> GetDomainList(int affId, bool isInternal)
        {
            return _marketingRepository.GetDomainList(affId, isInternal);
        }

        public IDictionary<string, string> GenerateLinkList(LinkGenerationParam linkGenerationParam)
        {
            var linkList = new Dictionary<string, string>();

            var languageList = linkGenerationParam.LanguageList;
            if (string.IsNullOrEmpty(linkGenerationParam.SelectedLanguage) &&
                languageList != null && languageList.Any())
            {
                foreach (var item in languageList)
                {
                    var language = SystemLanguage.LanguageList.FirstOrDefault(l => l.Code.MatchWith(item.Value));

                    if (language != null)
                    {
                        linkList.Add(language.NativeName, GenerateLink(linkGenerationParam, language.ShortedCode));
                    }
                }

                var preferredLanguage = SystemLanguage.LanguageList.FirstOrDefault(l => l.Code.MatchWith(linkGenerationParam.PreferredLanguage));
                linkList.Add(Lang.Default, GenerateLink(linkGenerationParam, preferredLanguage.ShortedCode));
            }
            else
            {
                var language = SystemLanguage.LanguageList.FirstOrDefault(l => l.Code.MatchWith(linkGenerationParam.SelectedLanguage));

                if (language != null)
                {
                    linkList.Add(language.NativeName, GenerateLink(linkGenerationParam, language.ShortedCode));
                }
            }

            return linkList;
        }

        public IEnumerable<MarketingItem> GetBannerList(OldMarketingBannerParam bannerParam)
        {
            const int LengthOfSplittingSize = 2;
            var uSpKey = IsSearchByCategory(bannerParam.Category, bannerParam.SubCategory) ? "Aff_BannerSelByParentCate" : "Aff_BannerSel_1";
            var bannerList = _marketingRepository.GetBannerList(bannerParam, uSpKey);

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

                var language = SystemLanguage.LanguageList.FirstOrDefault(l => l.EnglishName.MatchWith(banner.Language));

                if (language != null)
                {
                    banner.RedirectLink = GenerateLink(bannerParam, language.ShortedCode);
                }

            }

            return bannerList;
        }

        public IEnumerable<MarketingItem> GetTextList(OldMarketingTextParam textParam)
        {
            var textList = _marketingRepository.GetTextList(textParam);

            foreach (var text in textList)
            {
                text.Message = HttpContext.Current.Server.HtmlDecode(text.Message.Replace("\r\n", "<br/>"));
                text.ScriptSrc = textParam.ScriptUrl + text.BannerId;
                var language = SystemLanguage.LanguageList.FirstOrDefault(l => l.EnglishName.MatchWith(text.Language));

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

        private static bool IsSearchByCategory(string category, string subCategory)
        {
            return (category.MatchWith("sportsbook") || category.MatchWith("casino")) &&
                    string.IsNullOrEmpty(subCategory);
        }
    }
}
