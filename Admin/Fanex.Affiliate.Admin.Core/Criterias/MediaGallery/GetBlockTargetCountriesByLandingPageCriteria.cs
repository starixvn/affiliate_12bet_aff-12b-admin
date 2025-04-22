using Fanex.Data.Repository;

namespace Fanex.Affiliate.Admin.Core.Criterias.MediaGallery
{
    public class GetBlockTargetCountriesByLandingPageCriteria : CriteriaBase
    {
        const string SP_NAME = "Aff_LandingPage_ExcludedTargetCountry_Sel";

        public override string GetSettingKey() => SP_NAME;

        public override bool IsValid() => true;

        public int PageId { get; set; }
    }
}
