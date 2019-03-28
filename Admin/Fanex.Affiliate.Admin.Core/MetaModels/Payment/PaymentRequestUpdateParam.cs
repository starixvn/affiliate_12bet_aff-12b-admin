using System;

namespace Fanex.Affiliate.Admin.Core.MetaModels.Payment
{
    public class PaymentRequestUpdateParam
    {
        public long TransId { get; set; }

        public int AffId { get; set; }

        public decimal Amount { get; set; }

        public DateTime TransDate { get; set; }

        public string Status { get; set; }

        public string AdminDesc { get; set; }

        public string AffDesc { get; set; }

        public int UserId { get; set; }

        public string Ip { get; set; }
    }
}
