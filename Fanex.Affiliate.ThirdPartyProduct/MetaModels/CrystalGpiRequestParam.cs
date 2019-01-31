namespace Fanex.Affiliate.ThirdPartyProduct.MetaModels
{
    public class CrystalGpiRequestParam : RequestBaseParam
    {
        public string StartTime { get; set; }

        public string EndTime { get; set; }

        public int PageNumber { get; set; }

        public int PageSize { get; set; }
    }
}
