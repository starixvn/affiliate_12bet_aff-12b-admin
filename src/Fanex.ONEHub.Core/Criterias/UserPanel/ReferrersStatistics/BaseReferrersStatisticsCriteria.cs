using Fanex.Data.Repository;
using System;

namespace Fanex.Affiliate.Admin.Core.Criterias.UserPanel.ReferrersStatistics
{
    public abstract class BaseReferrersStatisticsCriteria : CriteriaBase
    {
        public int AffId { get; set; }

        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        public short DeviceType { get; set; }
    }
}
