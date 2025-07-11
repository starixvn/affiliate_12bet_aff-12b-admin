namespace Fanex.Affiliate.Admin.Core.DTO.Report
{
    public class AffiliateCpaReport
    {
        public string Month { get; set; }

        public string Affiliate { get; set; }

        public string DealName { get; set; }

        public string DealType { get; set; }

        public bool InstantPayment { get; set; }

        public string Quota { get; set; }

        public long CPAAmount { get; set; }

        public long EstimatedCPAAmount { get; set; }

        public long ApprovedCPAAmount { get; set; }
    }
}
