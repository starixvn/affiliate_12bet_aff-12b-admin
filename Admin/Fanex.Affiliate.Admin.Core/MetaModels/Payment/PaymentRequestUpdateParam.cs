using System;

namespace Fanex.Affiliate.Admin.Core.MetaModels.Payment
{
    public class PaymentRequestUpdateParam : AffiliateBasicInfoParam
    {
        public long TransId { get; set; }

        public decimal Amount { get; set; }

        public DateTime TransDate { get; set; }

        public string Status { get; set; }

        public string AdminDesc { get; set; }

        public string AffDesc { get; set; }
    }
}
