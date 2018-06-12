using System;

namespace Fanex.Affiliate.User.Core.MetalModels.UserPanel
{
    public class PaymentCreationParam
    {
        public int AffId { get; set; }

        public decimal Amount { get; set; }

        public string Ip { get; set; }

        public string Language { get; set; }

        public DateTime TransDate { get; set; }

        public long TransId { get; set; }

        public int ErrCode { get; set; }

        public string ErrMsg { get; set; }
    }
}
