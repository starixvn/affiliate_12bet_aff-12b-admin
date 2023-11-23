using Fanex.Affiliate.User.Core.DTO;
using Fanex.Affiliate.User.Core.DTO.UserPanel.MediaGallery;
using Fanex.Affiliate.User.Core.MetalModels.UserPanel;
using System.Collections.Generic;

namespace Fanex.Affiliate.User.Core.BusinessService
{
    public interface IMarketingService
    {
        IEnumerable<TargetProduct> GetTargetProductList(bool isInternal);

        IEnumerable<SizeType> GetSizeList(SizeParam param, bool isInternal);

        IEnumerable<Domain> GetDomainList(string targetCountry, bool isInternal);

        IDictionary<string, string> GenerateLinkList(LinkGenerationParam linkGenerationParam);

        IEnumerable<Banner> GetBannerList(MarketingBannerParam bannerParam);

        IEnumerable<LandingPage> GetSpecialLandingPageList(string targetCountry, string commGroupName, string language, bool isInternal);

        IEnumerable<LandingPage> GetLandingPageList(string targetCountry, string commGroupName, bool isInternal);

        ResultInfo CountNumberOfCopies(CopiedBannerCounterParam param);

        string GetTargetCountryCodeByAff(int affId);
    }
}
