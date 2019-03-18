using System;

namespace Fanex.Affiliate.Admin.Core.MetaModels.Payment
{
    public class PaymentRequestParam
    {
        public string Username { get; set; }

        public string Status { get; set; }

        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        public bool IsGetAll { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }
    }
}
