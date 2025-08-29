namespace Fanex.Affiliate.Admin.Core.DTO.Report
{
    using System;

    public class RedepositCpaReport
    {
        public int RowIdx { get; set; }

        public int AffId { get; set; }

        public string AffUserName { get; set; }

        public int DealId { get; set; }

        public string DealName { get; set; }

        public string PaymentFrequency { get; set; }

        public DateTime? StartFrom { get; set; }

        public string StartFromStr => StartFrom?.ToString("MM/dd/yyyy") ?? string.Empty;

        public DateTime? ApprovalDate { get; set; }

        public string ApprovalDateStr => ApprovalDate?.ToString("MM/dd/yyyy") ?? string.Empty;

        public int RecheckDays { get; set; }

        public DateTime? ExpiredRedepositDate { get; set; }

        public string ExpiredRedepositDateStr => ExpiredRedepositDate?.ToString("MM/dd/yyyy") ?? string.Empty;

        public int ApprovedCpaPlayer { get; set; }

        public decimal RedepositRate { get; set; }

        public int ApprovedCpaPlayerRate { get; set; }

        public string NumOfPlayerRedeposit { get; set; }

        public decimal TotalSettledCpaAmount { get; set; }

        public string Status { get; set; }
    }
}
