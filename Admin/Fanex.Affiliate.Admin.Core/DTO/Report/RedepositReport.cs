namespace Fanex.Affiliate.Admin.Core.DTO.Report
{
    using System;

    public class RedepositReport
    {
        public int AffId { get; set; }

        public string AffUserName { get; set; }

        public int DealId { get; set; }

        public string DealName { get; set; }

        public string PaymentFrequency { get; set; }

        public DateTime? StartDate { get; set; }

        public string StartDateStr => StartDate?.ToString("MM/dd/yyyy") ?? string.Empty;

        public DateTime? SettledDate { get; set; }

        public string SettledDateStr => SettledDate?.ToString("MM/dd/yyyy") ?? string.Empty;

        public int RecheckDays { get; set; }

        public DateTime? ExpiredRedepositDate { get; set; }

        public string ExpiredRedepositDateStr => ExpiredRedepositDate?.ToString("MM/dd/yyyy") ?? string.Empty;

        public int ApprovedCpaPlayer { get; set; }

        public decimal RedepositRate { get; set; }

        public int ApprovedCpaPlayerRate { get; set; }

        public int NumOfPlayerRedeposit { get; set; }

        public decimal TotalSettledCpaAmount { get; set; }

        public string Status { get; set; }
    }
}
