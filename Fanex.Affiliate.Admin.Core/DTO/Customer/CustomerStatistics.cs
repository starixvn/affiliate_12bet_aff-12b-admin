namespace Fanex.Affiliate.Admin.Core.DTO.Customer
{
    public class CustomerStatistics
    {
        public int NewSignUp { get; set; }

        public int NewFdm { get; set; }

        public int TotalNewSignUp { get; set; }

        public int TotalNewFdm { get; set; }

        public int TotalActivePlayer { get; set; }

        public int TotalQualifiedActivePlayer { get; set; }

        public decimal TotalDeposit { get; set; }

        public decimal TotalDepositUp { get; set; }

        public decimal Turnover { get; set; }

        public decimal TotalTurnover { get; set; }

        public decimal Winlost { get; set; }

        public decimal TotalWinlost { get; set; }

        public int UniqueClick { get; set; }

        public int TotalClick { get; set; }

        public int UniqueClickByPcLaptop { get; set; }

        public int TotalClickByPcLaptop { get; set; }

        public int UniqueClickByMobile { get; set; }

        public int TotalClickByMobile { get; set; }
    }
}
