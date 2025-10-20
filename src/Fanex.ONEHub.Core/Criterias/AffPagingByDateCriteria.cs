
using System;

namespace Fanex.Affiliate.Admin.Core.Criterias
{
    public abstract class AffPagingByDateCriteria : AffPagingCriteria
    {
        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }
    }
}
