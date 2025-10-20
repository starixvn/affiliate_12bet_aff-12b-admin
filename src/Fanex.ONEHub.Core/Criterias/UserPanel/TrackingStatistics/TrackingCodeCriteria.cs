using Fanex.Data.Repository;

namespace Fanex.Affiliate.Admin.Core.Criterias.UserPanel.TrackingStatistics
{
    public class TrackingCodeCriteria : CriteriaBase
    {
        const string SP_NAME = "Aff_AffiliateSelTrackingCode";

        public override string GetSettingKey() => SP_NAME;

        public override bool IsValid() => AffId > 0;

        public int AffId { get; set; }
    }
}
