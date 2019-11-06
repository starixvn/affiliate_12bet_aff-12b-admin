using System.Collections.Generic;

namespace Fanex.Affiliate.DataService.Importer.DTO.ISoftbet
{
    public class ISoftbetSummary
    {
        public ISoftbetResultInfo ResultInfo { get; set; }

        public IEnumerable<ISoftbetBetHistory> BetHistoryList { get; set; }
    }
}
