namespace Fanex.Affiliate.User.Core.Criterias
{
    using System;

    public abstract class AffPagingByDateCriteria : AffPagingCriteria
    {
        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }
    }

}