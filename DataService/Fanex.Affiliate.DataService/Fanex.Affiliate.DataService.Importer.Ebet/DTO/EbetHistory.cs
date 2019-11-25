namespace Fanex.Affiliate.DataService.Importer.Ebet.DTO
{
    public class EbetHistory
    {
        public string BetHistoryId { get; set; }

        public string UserName { get; set; }

        public decimal Bet { get; set; }

        public decimal Payout { get; set; }

        public long PayoutUnixTime { get; set; }

        public long CreateUnixTime { get; set; }

    }
}
