using System;

namespace Fanex.Affiliate.User.Core.DTO.UserPanel
{
    public class PlayerDetail
    {
        public string Username { get; set; }

        public DateTime RegisteredDate { get; set; }

        public decimal Turnover { get; set; }

        public decimal Winloss { get; set; }

        public decimal Bonus { get; set; }

        public decimal Deposit { get; set; }

        public decimal Withdrawal { get; set; }

        public decimal BankingFee { get; set; }
    }
}
