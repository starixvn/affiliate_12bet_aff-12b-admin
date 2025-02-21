namespace Fanex.Affiliate.User.Core.BusinessService
{
    using System.Collections.Generic;
    using System.Linq;
    using DTO;
    using DTO.UserPanel;
    using DTO.UserPanel.MediaGallery;
    using Enum;
    using MetalModels;
    using MetalModels.UserPanel;
    using ViewModels.UserPanel.Marketing;

    public interface IMarketingService
    {
        IEnumerable<CategoryType> GetCategoryList(bool isMediaPage, AffiliateLinkType linkType, bool isInternal = false);

        IEnumerable<CurrencyType> GetCurrencyList(bool isInternal);

        IEnumerable<SizeType> GetSizeList(SizeParam param, bool isInternal = false);

        IEnumerable<Domain> GetAvailableDomainList(int affId, AffiliateLinkType linkType, bool isInternal = false);

        IDictionary<string, LanguageLinkViewModel> GenerateLinkList(LinkGeneratingParam linkGeneratedParam);

        IEnumerable<MarketingItem> GetBannerList(MarketingBannerParam bannerParam);

        IEnumerable<LandingPage> GetLandingPageList(string commGroupName, bool isInternal = false, AffiliateLinkType linkType = AffiliateLinkType.AffiliateLink);

        ResultInfo<MarketingChannel> AddMarketingChannel(int userId, string channelName);

        IDictionary<string, string> GetChannels(int userId);

        string GetTextLinkTemplate(AffiliateLinkType linkType);

        ResultInfo ToggleMarketingLink(MarketingLinkParam param);

        IEnumerable<IGrouping<string, MarketingChannelLink>> GetUsingMarketingLinks(int userId);
    }
}