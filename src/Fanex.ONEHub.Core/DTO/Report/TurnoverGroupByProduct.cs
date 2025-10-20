namespace Fanex.Affiliate.Admin.Core.DTO.Report
{
    using System.Collections.Generic;

    public class TurnoverGroupByProduct
    {
        public int MemberCPAId { get; set; }

        public int MemberCPATierId { get; set; }

        public int GroupProductId { get; set; }

        public decimal Turnover { get; set; }

        public decimal CPAAmount { get; set; }

        public IEnumerable<TurnoverGroupByProductDetail> Products { get; set; }
    }
}
