using System.Collections.Generic;

namespace Fanex.Affiliate.DataService.Importer.DTO.Ebet
{
    public class EbetSummary
    {
        public int CurrentcyId { get; set; }

        public int Count { get; set; }

        public int RemainingVisits { get; set; }

        public int Status { get; set; }

        public IEnumerable<EbetHistory> BetHistories { get; set; }
    }
}
