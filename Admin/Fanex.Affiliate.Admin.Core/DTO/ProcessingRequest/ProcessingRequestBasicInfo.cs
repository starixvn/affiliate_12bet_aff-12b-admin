using System;

namespace Fanex.Affiliate.Admin.Core.DTO.ProcessingRequest
{
    public class ProcessingRequestBasicInfo
    {
        public int RequestId { get; set; }

        public int AffId { get; set; }

        public string UserName { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? CreationDate { get; set; }

        public DateTime? LastModified { get; set; }

        public DateTime? SubmittedDate { get; set; }

        public DateTime Deadline { get; set; }

        public string ApprovedBy { get; set; }

        public DateTime? ApprovalDate { get; set; }

        public string Status { get; set; }
    }
}
