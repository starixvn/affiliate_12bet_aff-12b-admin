namespace Fanex.Affiliate.Admin.Core.DTO.ProcessingRequest
{
    public class ProcessingRequestSummaryInfo
    {
        public int Draft { get; set; }

        public int KIV { get; set; }

        public int Submitted { get; set; }

        public int Approved { get; set; }

        public int Rejected { get; set; }
    }
}
