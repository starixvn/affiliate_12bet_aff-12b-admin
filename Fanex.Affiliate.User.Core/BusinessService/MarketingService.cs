using System;
using System.Collections.Generic;
using System.Linq;
using Fanex.Affiliate.User.Core.Constants;
using Fanex.Affiliate.User.Core.DTO.UserPanel;
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

            _cacheService.Set(CacheKey.BannerTypeList, bannerTypeList);

            return bannerTypeList;
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
                    var language = SystemLanguage.LanguageList.FirstOrDefault(l => string.Compare(l.Code, item.Value, true) == 0);

                    if (language != null)
                    {
                        linkList.Add(language.NativeName, GenerateLink(linkGenerationParam, language.ShortedCode));
                    }
                }

                linkList.Add(Lang.Default, GenerateLink(linkGenerationParam, linkGenerationParam.PreferredLanguage));
            }
            else
            {
                var language = SystemLanguage.LanguageList.FirstOrDefault(l => string.Compare(l.Code, linkGenerationParam.SelectedLanguage, true) == 0);

                if (language != null)
                {
                    linkList.Add(language.NativeName, GenerateLink(linkGenerationParam, language.ShortedCode));
                }
            }

            return linkList;
        }

        public IEnumerable<MarketingItem> GetBannerList(MarketingBannerParam bannerParam)
        {
            var bannerList = _marketingRepository.GetBannerList(bannerParam);

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

                var language = SystemLanguage.LanguageList.FirstOrDefault(l => string.Compare(l.EnglishName, banner.Language, true) == 0);

                if (language != null)
                {
                    banner.RedirectLink = GenerateLink(bannerParam, language.ShortedCode);
                }

            }

            return bannerList;
        }

        private string GenerateLink(LinkBaseParam linkGenerationParam, string shortedLanguageCode)
        {
            var selectedDomain = linkGenerationParam.SelectedDomain;
            var template = linkGenerationParam.TextLinkTemplate;
            var scheme = selectedDomain.IsSsl ? Uri.UriSchemeHttps : Uri.UriSchemeHttp;

            var result = template.Replace("{scheme}", scheme)
                                .Replace("{domain}", selectedDomain.DomainName)
                                .Replace("{custid}", linkGenerationParam.AffId.ToString())
                                .Replace("{target}", linkGenerationParam.SelectedPage)
                                .Replace("{lang}", shortedLanguageCode);

            return result;
        }
    }
}