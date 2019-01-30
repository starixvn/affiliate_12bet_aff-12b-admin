namespace Fanex.Affiliate.ThirdPartyProduct.MetaModels
{
    public class CrystalGpiRequestParam
    {
        public string StartTime { get; set; }

        public string EndTime { get; set; }

        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public bool IsGetByApiTimeZone { get; set; }
    }
}
