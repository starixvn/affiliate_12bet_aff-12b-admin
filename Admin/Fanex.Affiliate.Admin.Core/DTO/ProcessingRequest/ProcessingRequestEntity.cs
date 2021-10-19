using System.Collections.Generic;

namespace Fanex.Affiliate.Admin.Core.DTO.ProcessingRequest
{
    public class ProcessingRequestEntity
    {
        public IEnumerable<ProcessingRequestBasicInfo> MainData { get; set; }

        public ProcessingRequestSummaryInfo Summary { get; set; }

        public int TotalItem { get; set; }
    }
}
