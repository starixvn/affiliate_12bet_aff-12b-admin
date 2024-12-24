namespace Fanex.Affiliate.User.Core.Criterias
{
    using System;
    using Data;
    using Data.Repository;

    public abstract class BaseStatisticsCriteria : CriteriaBase
    {
        public int AffId { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public byte DeviceType { get; set; }

        [SpParam(Ignored = true)]
        public bool IsInternal { get; set; }
    }
}
