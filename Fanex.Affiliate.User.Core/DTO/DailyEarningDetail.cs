namespace Fanex.Affiliate.User.Core.DTO
{
    public class DailyEarningDetail
    {
        public int CustId { get; set; }

        public string UserName { get; set; }

        public decimal WinLost { get; set; }

        public decimal Withdrawal { get; set; }

        public decimal Turnover { get; set; }

        public decimal Deposit { get; set; }
    }
}
