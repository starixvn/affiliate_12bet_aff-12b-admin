using Fanex.Affiliate.User.Core.DTO;
using Fanex.Affiliate.User.Core.DTO.UserPanel.MediaGallery;
using Fanex.Affiliate.User.Core.MetalModels.UserPanel;
using System.Collections.Generic;

namespace Fanex.Affiliate.User.Core.Repository.UserPanel
{
    public interface IMarketingRepository
    {
        IEnumerable<LandingPage> GetLandingPageList(bool isInternal);





        IEnumerable<TargetProduct> GetTargetProductList(bool isInternal);

        IEnumerable<SizeType> GetSizeList(SizeParam param, bool isInternal);

        IEnumerable<Domain> GetDomainListByTargetCountry(string targetCountryCode, bool isInternal);

        IEnumerable<Banner> GetBannerList(MarketingBannerParam bannerParam);

        IEnumerable<LandingPage> GetLandingPageListByTargetCountry(string targetCountryCode, bool isInternal);

        ResultInfo CountNumberOfCopies(CopiedBannerCounterParam param);
    }
}
