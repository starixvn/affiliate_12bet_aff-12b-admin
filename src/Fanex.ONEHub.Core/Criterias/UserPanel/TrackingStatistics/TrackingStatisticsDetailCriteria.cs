
namespace Fanex.Affiliate.Admin.Core.Criterias.UserPanel.TrackingStatistics
{
    public class TrackingStatisticsDetailCriteria : BaseTrackingStatisticsCriteria
    {
        const string SP_NAME = "Aff_AffiliateSelClickStatisticsMonthDetail";

        public override string GetSettingKey() => SP_NAME;

        public override bool IsValid() => AffId > 0;

        public int Month { get; set; }

        public int Year { get; set; }
    }
}
