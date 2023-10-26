using Fanex.Affiliate.User.Core.DTO;
using Fanex.Affiliate.User.Core.DTO.UserPanel.MediaGallery;
using Fanex.Affiliate.User.Core.MetalModels.UserPanel;
using System.Collections.Generic;

namespace Fanex.Affiliate.User.Core.BusinessService
{
    public interface IMarketingService
    {
        IEnumerable<TargetProduct> GetTargetProductList(bool isInternal);

        IEnumerable<Language> GetLanguageList();

        IEnumerable<SizeType> GetSizeList(SizeParam param, bool isInternal);

        IEnumerable<Domain> GetDomainList(int affId, bool isInternal);

        IDictionary<string, string> GenerateLinkList(LinkGenerationParam linkGenerationParam);

        IEnumerable<Banner> GetBannerList(MarketingBannerParam bannerParam);

        IEnumerable<LandingPage> GetSpecialLandingPageList(string commGroupName, string targetCountry, string language, bool isInternal);

        IEnumerable<LandingPage> GetLandingPageList(string commGroupName, bool isInternal);

        ResultInfo CountNumberOfCopies(CopiedBannerCounterParam param);
    }
}
