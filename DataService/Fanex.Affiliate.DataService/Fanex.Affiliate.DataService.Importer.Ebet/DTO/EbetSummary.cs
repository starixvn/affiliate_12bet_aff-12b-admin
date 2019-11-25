using System.Collections.Generic;

namespace Fanex.Affiliate.DataService.Importer.Ebet.DTO
{
    public class EbetSummary
    {
        public EbetResultInfo ResultInfo { get; set; }

        public IEnumerable<EbetHistory> BetHistories { get; set; }
    }
}
