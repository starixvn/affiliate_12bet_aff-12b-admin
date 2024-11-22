namespace Fanex.Affiliate.User.Core.BusinessService
{
    using System.Collections.Generic;
    using DTO;
    using DTO.UserPanel.AccountOverview;
    using DTO.UserPanel.MediaGallery;
    using Enum;
    using MetalModels.UserPanel;

    public interface IMarketingService
    {
        IEnumerable<LandingPage> GetLandingPageList(bool isInternal = false, AffiliateLinkType linkType = AffiliateLinkType.AffiliateLink);




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
