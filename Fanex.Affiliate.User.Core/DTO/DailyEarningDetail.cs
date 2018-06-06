namespace Fanex.Affiliate.User.Core.DTO
{
    public class DailyEarningDetail
    {
        public int CustId { get; set; }

        public string UserName { get; set; }

        public string IbcUserName { get; set; }

        public decimal WinLost { get; set; }

        public decimal Withdrawal { get; set; }

        public decimal Turnover { get; set; }

        public decimal Deposit { get; set; }

        public decimal Bonus { get; set; }

        public decimal Adjustment { get; set; }

        public string UserName2
        {
            get
            {
                return UserName.ToLower().StartsWith("12bet$") ? UserName.Substring(6, UserName.Length - 6) : UserName;
            }
        }
    }
}
