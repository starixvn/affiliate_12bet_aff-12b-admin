using System.Collections.Generic;

namespace Fanex.Affiliate.DataService.Importer.DTO.Quartz
{
    public class QuartzSummary
    {
        public QuartzResultInfo ResultInfo { get; set; }

        public IEnumerable<QuartzBetHistory> BetHistoryList { get; set; }
    }
}
