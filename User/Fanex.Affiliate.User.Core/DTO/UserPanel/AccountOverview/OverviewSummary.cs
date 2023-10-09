namespace Fanex.Affiliate.User.Core.DTO.UserPanel.AccountOverview
{
    public class OverviewSummary
    {
        public int Signups { get; set; }

        public int DepositedMembers { get; set; }

        public decimal BalanceBroughtForward { get; set; }

        public int ActivePlayers { get; set; }

        public int QualifiedActivePlayers { get; set; }

        public decimal Turnover { get; set; }

        public decimal Deposit { get; set; }

        public decimal Commission { get; set; }

        public decimal FDMCommission { get; set; }

        public decimal FdmDeposit { get; set; }
    }
}
