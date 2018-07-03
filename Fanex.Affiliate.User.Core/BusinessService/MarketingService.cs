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
    public class MarketingService : IMarketingService
    {
        private readonly IMarketingRepository _marketingRepository;

        private readonly IMemoryCacheService _cacheService;

        public MarketingService(IMarketingRepository marketingRepository, IMemoryCacheService cacheService)
        {
            _marketingRepository = marketingRepository;
            _cacheService = cacheService;
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

        public IEnumerable<MarketingItem> GetBannerList(MarketingBannerParam bannerParam)
        {
            var uSpKey = IsSearchByCategory(bannerParam.Category, bannerParam.SubCategory) ? "Aff_BannerSelByParentCate" : "Aff_BannerSel_1";
            var bannerList = _marketingRepository.GetBannerList(bannerParam, uSpKey);

            foreach (var banner in bannerList)
            {
                banner.FileName = bannerParam.BannerDomain + banner.FileName;
                banner.SelectedDomainName = bannerParam.SelectedDomain.DomainName;

                var sizes = banner.Size.Split('x');
                if (sizes.Count() == 2)
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

        public IEnumerable<MarketingItem> GetTextList(MarketingTextParam textParam)
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

        private string GenerateLink(LinkBaseParam linkParam, string shortedLanguageCode)
        {
            var selectedDomain = linkParam.SelectedDomain;
            var template = linkParam.TextLinkTemplate;
            var scheme = selectedDomain.IsSsl ? Uri.UriSchemeHttps : Uri.UriSchemeHttp;

            var result = template.Replace("{scheme}", scheme)
                                .Replace("{domain}", selectedDomain.DomainName)
                                .Replace("{custid}", linkParam.AffId.ToString())
                                .Replace("{target}", linkParam.SelectedPage)
                                .Replace("{lang}", shortedLanguageCode);

            return result;
        }

        private bool IsSearchByCategory(string category, string subCategory)
        {
            return (category.MatchWith("sportsbook") || category.MatchWith("casino")) &&
                    string.IsNullOrEmpty(subCategory);
        }
    }
}