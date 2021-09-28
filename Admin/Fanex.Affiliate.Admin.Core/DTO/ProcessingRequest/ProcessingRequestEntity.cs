using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fanex.Affiliate.Admin.Core.DTO.ProcessingRequest
{
    public class ProcessingRequestEntity
    {
        public IEnumerable<ProcessingRequestBasicInfo> MainData { get; set; }

        public ProcessingRequestSummaryInfo Summary { get; set; }

        public int TotalItem { get; set; }
    }
}
