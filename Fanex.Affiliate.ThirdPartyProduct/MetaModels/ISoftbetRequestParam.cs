namespace Fanex.Affiliate.ThirdPartyProduct.MetaModels
{
    public class ISoftbetRequestParam
    {
        public string StartTime { get; set; }

        public string EndTime { get; set; }

        public int PageNumber { get; set; }

        public bool IsGetByApiTimeZone { get; set; }
    }
}
