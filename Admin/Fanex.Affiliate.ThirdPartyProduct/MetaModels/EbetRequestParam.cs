namespace Fanex.Affiliate.ThirdPartyProduct.MetaModels
{
    public class EbetRequestParam
    {
        public string CurrencyId { get; set; }

        public string UserName { get; set; } = string.Empty;

        public string StartTime { get; set; }

        public string EndTime { get; set; }

        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public bool IsGetByApiTimeZone { get; set; }
    }
}
