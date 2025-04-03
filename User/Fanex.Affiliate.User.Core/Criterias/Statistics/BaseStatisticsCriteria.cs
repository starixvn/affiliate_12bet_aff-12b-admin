namespace Fanex.Affiliate.User.Core.Criterias
{
    using System;

    public abstract class BaseStatisticsCriteria : BaseCriteria
    {
        public int AffId { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public short DeviceType { get; set; }
    }
}
