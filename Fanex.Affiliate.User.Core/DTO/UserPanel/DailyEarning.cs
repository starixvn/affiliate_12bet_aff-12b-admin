using System;

namespace Fanex.Affiliate.User.Core.DTO.UserPanel
{
    public class DailyEarning
    {
        public DateTime WinlostDate { get; set; }

        public decimal Deposit { get; set; }

        public decimal Withdrawal { get; set; }

        public decimal MemberWinLost { get; set; }

        public decimal MemberWinLostFake { get; set; }

        public decimal Bonus { get; set; }

    }
}
