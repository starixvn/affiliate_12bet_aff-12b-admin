using Fanex.Data;

namespace Fanex.Affiliate.User.Core.MetalModels.UserPanel
{
    public class MarketingBannerParam : LinkBaseParam
    {
        public string Category { get; set; }

        public string Language { get; set; }

        public string ImageType { get; set; }

        public string Size { get; set; }

        public string Currency { get; set; }

        public string AffType { get; set; }

        public int? PageSize { get; set; }

        public int? PageIndex { get; set; }

        public byte? DeviceType { get; set; }

        [SpParam(Ignored = true)]
        public int TotalPages { get; set; }

        public int Nrows { get; set; }

        [SpParam(Ignored = true)]
        public string BannerDomain { get; set; }

        [SpParam(Ignored = true)]
        public string SubCategory { get; set; }

        [SpParam(Ignored = true)]
        public int ScreenWidth { get; set; }
    }
}
