using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fanex.Affiliate.Admin.Core.MetaModels.ProcessingRequest
{
    public class ProcessingRequestDelParam : UserInfoParam
    {
        public int RequestId { get; set; }

        public string Language { get; set; }
    }
}
