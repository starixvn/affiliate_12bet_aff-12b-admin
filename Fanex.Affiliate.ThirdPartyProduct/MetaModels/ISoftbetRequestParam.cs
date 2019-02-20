namespace Fanex.Affiliate.ThirdPartyProduct.MetaModels
{
    public class ISoftbetRequestParam : RequestBaseParam
    {
        public string StartTime { get; set; }

        public string EndTime { get; set; }

        public int PageNumber { get; set; }
    }
}
