using Fanex.Affiliate.User.Core.DTO;
using Fanex.Affiliate.User.Core.DTO.UserPanel;
using Fanex.Affiliate.User.Core.DTO.UserPanel.MediaGallery;
using Fanex.Affiliate.User.Core.MetalModels;
using Fanex.Affiliate.User.Core.MetalModels.UserPanel;
using System.Collections.Generic;

namespace Fanex.Affiliate.User.Core.Repository.UserPanel
{
    public interface IMarketingRepository
    {
        IEnumerable<CategoryType> GetCategoryList(MediaCategoryParam param, bool isInternal);

        IEnumerable<CurrencyType> GetCurrencyList(bool isInternal);

        IEnumerable<SizeType> GetSizeList(SizeParam param, bool isInternal);

        IEnumerable<Domain> GetAvailableDomainList(int affId, bool isMember, bool isInternal);

        IEnumerable<MarketingItem> GetBannerList(MarketingBannerParam bannerParam);

        IEnumerable<LandingPage> GetLandingPageList(bool isInternal);

        ResultInfo AddMarketingChannel(int userId, string channelCode, string channelName);

        IEnumerable<MarketingChannel> GetMarketingChannels(int userId);

        ResultInfo ToggleMarketingLink(MarketingLinkParam param);

        IEnumerable<MarketingChannelLink> GetUsingMarketingLink(int userId);
    }
}
