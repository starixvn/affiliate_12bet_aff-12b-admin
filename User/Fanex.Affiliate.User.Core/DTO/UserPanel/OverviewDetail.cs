using System;

namespace Fanex.Affiliate.User.Core.DTO.UserPanel
{
    public class OverviewDetail
    {
        public DateTime Date { get; set; }

        public int Signups { get; set; }

        public int DepositMembers { get; set; }

        public int ActivePlayers { get; set; }

        public int QualifiedActivePlayers { get; set; }

        public decimal Deposit { get; set; }

        public decimal Withdrawal { get; set; }

        public decimal Turnover { get; set; }

        public decimal Winloss { get; set; }
    }
}
