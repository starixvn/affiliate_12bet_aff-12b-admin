using Fanex.Data.Repository;

namespace Fanex.Affiliate.Admin.Core.Criterias.UserPanel.TrackingStatistics
{
    public abstract class BaseTrackingStatisticsCriteria : CriteriaBase
    {
        public int AffId { get; set; }

        public string TrackingCode { get; set; }
    }
}
