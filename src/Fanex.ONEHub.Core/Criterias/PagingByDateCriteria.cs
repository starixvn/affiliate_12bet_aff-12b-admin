using System;

namespace Fanex.Affiliate.Admin.Core.Criterias
{
    public abstract class PagingByDateCriteria : PagingCriteria
    {
        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }
    }
}
