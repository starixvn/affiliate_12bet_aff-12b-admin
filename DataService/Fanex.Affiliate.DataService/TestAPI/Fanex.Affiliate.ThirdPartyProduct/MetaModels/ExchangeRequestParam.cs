namespace Fanex.Affiliate.ThirdPartyProduct.MetaModels
{
    public class ExchangeRequestParam
    {
        public int CustId { get; set; }

        public string StartTime { get; set; }

        public string EndTime { get; set; }

        public int PageNumber { get; set; }

        public string Environment { get; set; }

        public bool IsGetByApiTimeZone { get; set; }
    }
}
