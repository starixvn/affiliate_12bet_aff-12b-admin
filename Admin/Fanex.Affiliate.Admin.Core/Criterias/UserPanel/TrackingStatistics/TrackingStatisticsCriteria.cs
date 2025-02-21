
namespace Fanex.Affiliate.Admin.Core.Criterias.UserPanel.TrackingStatistics
{
    public class TrackingStatisticsCriteria : BaseTrackingStatisticsCriteria
    {
        const string SP_NAME = "Aff_AffiliateSelClickStatisticsAllMonths";

        public override string GetSettingKey() => SP_NAME;

        public override bool IsValid() => AffId > 0;
    }
}
