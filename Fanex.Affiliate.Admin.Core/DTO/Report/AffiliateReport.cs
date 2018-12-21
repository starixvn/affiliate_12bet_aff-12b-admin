using System;
using Newtonsoft.Json;

namespace Fanex.Affiliate.Admin.Core.DTO.Report
{
    public class AffiliateReport
    {
        public int AffId { get; set; }

        public string UserName { get; set; }

        [JsonIgnore]
        public DateTime? MonthDate { get; set; }

        public string Description { get; set; }

        public string MonthDateString => MonthDate.HasValue ? MonthDate.Value.ToString("MM/yyyy") : string.Empty;

        public int TotalClick { get; set; }

        public int UniqueClick { get; set; }

        public int Signup { get; set; }

        public int Fdm { get; set; }

        public int ActiveMember { get; set; }

        public int QualifiedMember { get; set; }

        public decimal Deposit { get; set; }

        public decimal Withdrawal { get; set; }

        public decimal Turnover { get; set; }

        public decimal Winlost { get; set; }

        public decimal Bonus { get; set; }

        public decimal PlatformFee { get; set; }

        public decimal OperationalFee { get; set; }

        public decimal BankingFee { get; set; }

        public decimal TotalFee { get; set; }

        public decimal CommissionRate { get; set; }

        public decimal Earning { get; set; }

        public decimal AffWithdrawal { get; set; }

        public decimal AffAdjustment { get; set; }

        public decimal BalanceBf { get; set; }
    }
}
