using System.Collections.Generic;
using Fanex.Affiliate.User.Core.DTO.UserPanel;
using Fanex.Affiliate.User.Core.MetalModels.UserPanel;

namespace Fanex.Affiliate.User.Core.Repository.UserPanel
{
    public interface IOldMarketingRepository
    {
        IEnumerable<OldBannerType> GetBannerTypeList(bool isInternal);

        IEnumerable<Domain> GetDomainList(int affId, bool isInternal);

        IEnumerable<MarketingItem> GetBannerList(OldMarketingBannerParam bannerParam, string uSpKey);

        IEnumerable<MarketingItem> GetTextList(OldMarketingTextParam textParam);

        IEnumerable<LandingPage> GetLandingPageList(bool isInternal);
    }
}
