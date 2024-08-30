using System.Collections.Generic;
using Fanex.Affiliate.User.Core.DTO;
using Fanex.Affiliate.User.Core.DTO.UserPanel.MediaGallery;
using Fanex.Affiliate.User.Core.MetalModels.UserPanel;
using Fanex.Affiliate.User.Core.DTO.UserPanel.AccountOverview;

namespace Fanex.Affiliate.User.Core.BusinessService
{
    public interface IMarketingService
    {
        IEnumerable<TargetProduct> GetTargetProductList(bool isInternal);

        IEnumerable<SizeType> GetSizeList(SizeParam param, bool isInternal);

        IEnumerable<Domain> GetDomainListByTargetCountry(string targetCountry, int affId, bool isInternal);

        IEnumerable<AffiliateLink> GenerateLinkList(LinkGenerationParam linkGenerationParam);

        IEnumerable<Banner> GetBannerList(MarketingBannerParam bannerParam);

        IEnumerable<LandingPage> GetLandingPageList(string targetCountry, string commGroupName, bool isInternal);

        ResultInfo CountNumberOfCopies(CopiedBannerCounterParam param);

        string GetTargetCountryCodeByAff(int affId);
    }
}
