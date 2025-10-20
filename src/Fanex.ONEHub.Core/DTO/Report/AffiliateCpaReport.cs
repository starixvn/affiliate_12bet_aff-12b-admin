namespace Fanex.Affiliate.Admin.Core.DTO.Report
{
    public class AffiliateCpaReport
    {
        public string Month { get; set; }

        public int AffId { get; set; }

        public string Affiliate { get; set; }

        public int GroupId { get; set; }

        public string DealName { get; set; }

        public string DealType { get; set; }

        public byte ScheduleId { get; set; }

        public string PaymentFrequency { get; set; }

        public string Quota { get; set; }

        public long CPAAmount { get; set; }

        public long EstimatedCPAAmount { get; set; }

        public long ApprovedCPAAmount { get; set; }
    }
}