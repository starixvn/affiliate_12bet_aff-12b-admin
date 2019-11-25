using System;

namespace Fanex.Affiliate.DataService.Importer.ISoftbet.DTO
{
    public class ISoftbetBetHistory
    {
        public string PlayerId { get; set; }

        public string Currency { get; set; }

        public string SessionId { get; set; }

        public string GameId { get; set; }

        public string RoundId { get; set; }

        public string TransactionId { get; set; }

        public string Type { get; set; }

        public DateTime Time { get; set; }

        public decimal Amount { get; set; }
    }
}
