using System.Collections.Generic;
using Fanex.Affiliate.User.Core.DTO.UserPanel;
using Fanex.Affiliate.User.Core.MetalModels.UserPanel;

namespace Fanex.Affiliate.User.Core.Repository.UserPanel
{
    public interface IMarketingRepository
    {
        IEnumerable<BannerType> GetBannerTypeList(bool isInternal);

        IEnumerable<Domain> GetDomainList(int affId, bool isInternal);

        IEnumerable<MarketingItem> GetBannerList(MarketingBannerParam bannerParam);
    }
}
