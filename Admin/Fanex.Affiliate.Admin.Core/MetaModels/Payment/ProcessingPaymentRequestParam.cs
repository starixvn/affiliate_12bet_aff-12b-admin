using Fanex.Affiliate.Admin.Core.Constants;
using System.ComponentModel.DataAnnotations;

namespace Fanex.Affiliate.Admin.Core.MetaModels.Payment
{
    public class ProcessingPaymentRequestParam
    {
        public int SearchBy { get; set; }

        public string Status { get; set; }

        public string FromDate { get; set; }
    }
}
