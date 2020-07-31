using System;

namespace Fanex.Affiliate.ThirdPartyProduct.MetaModels
{
    public class ImportQueueParam
    {
        public int ProductId { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public byte JobType { get; set; }
    }
}
