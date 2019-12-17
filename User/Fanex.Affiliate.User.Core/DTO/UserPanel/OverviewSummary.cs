namespace Fanex.Affiliate.User.Core.DTO.UserPanel
{
    public class OverviewSummary
    {
        public int Signups { get; set; }

        public int DepositMembers { get; set; }

        public decimal BalanceBroughtFoward { get; set; }

        public int ActivePlayers { get; set; }

        public int QualifiedActivePlayers { get; set; }

        public decimal Turnover { get; set; }

        public decimal Deposit { get; set; }

        public decimal Commission { get; set; }
    }
}
