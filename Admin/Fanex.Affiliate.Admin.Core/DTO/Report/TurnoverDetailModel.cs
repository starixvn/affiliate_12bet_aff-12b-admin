namespace Fanex.Affiliate.Admin.Core.DTO.Report
{
    using System.Collections.Generic;

    public class TurnoverDetailModel
    {
        public TurnoverGroupByProduct Total { get; set; }

        public IEnumerable<TurnoverGroupByProduct> Groups { get; set; }
    }
}
