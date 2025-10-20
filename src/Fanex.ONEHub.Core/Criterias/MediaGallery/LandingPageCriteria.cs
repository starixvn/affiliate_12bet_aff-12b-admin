using Fanex.Data.Repository;

namespace Fanex.Affiliate.Admin.Core.Criterias.MediaGallery
{
    public class LandingPageCriteria : CriteriaBase
    {
        const string SP_NAME = "Aff_Admin_LandingPage_Sel";

        public override string GetSettingKey() => SP_NAME;

        public override bool IsValid() => true;

        public int? PageId { get; set; }
    }
}
