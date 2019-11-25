using System.Collections.Generic;

namespace Fanex.Affiliate.DataService.Importer.ISoftbet.DTO
{
    public class ISoftbetSummary
    {
        public ISoftbetResultInfo ResultInfo { get; set; }

        public IEnumerable<ISoftbetBetHistory> BetHistoryList { get; set; }
    }
}
