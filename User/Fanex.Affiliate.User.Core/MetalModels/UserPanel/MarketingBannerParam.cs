using Fanex.Data;

namespace Fanex.Affiliate.User.Core.MetalModels.UserPanel
{
    public class MarketingBannerParam : LinkBaseParam
    {
        public string TargetProduct { get; set; }

        [SpParam(Ignored = true)]
        public string Language { get; set; }

        public string Languages { get; set; }

        public string Size { get; set; }

        public string AffType { get; set; }

        public int? PageSize { get; set; }

        public int? PageIndex { get; set; }

        [SpParam(Ignored = true)]
        public int TotalPages { get; set; }

        public int Nrows { get; set; }

        [SpParam(Ignored = true)]
        public string BannerDomain { get; set; }
    }
}
