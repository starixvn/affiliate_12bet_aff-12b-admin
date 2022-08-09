using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fanex.Affiliate.Admin.Core.DTO.FeedbackReport
{
    public class FeedbackReportInfo
    {
        public string AffName { get; set; }

        public byte Rate { get; set; }

        public string Message { get; set; }

        public DateTime SentDate { get; set; }
    }
}
