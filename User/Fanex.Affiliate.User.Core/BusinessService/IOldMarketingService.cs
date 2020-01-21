using System.Collections.Generic;
using Fanex.Affiliate.User.Core.DTO.UserPanel;
using Fanex.Affiliate.User.Core.MetalModels.UserPanel;

namespace Fanex.Affiliate.User.Core.BusinessService
{
    public interface IOldMarketingService
    {
        IEnumerable<OldBannerType> GetBannerTypeList(bool isInternal);

        IEnumerable<Domain> GetDomainList(int affId, bool isInternal);

        IDictionary<string, string> GenerateLinkList(LinkGenerationParam linkGenerationParam);

        IEnumerable<MarketingItem> GetBannerList(OldMarketingBannerParam bannerParam);

        IEnumerable<MarketingItem> GetTextList(OldMarketingTextParam textParam);

        IEnumerable<LandingPage> GetLandingPageList(bool isInternal);
    }
}
