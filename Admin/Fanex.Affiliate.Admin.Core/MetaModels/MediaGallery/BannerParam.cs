
namespace Fanex.Affiliate.Admin.Core.MetaModels.MediaGallery
{
    public class BannerParam : AffiliatePagingByDateParam
    {
        public string Category { get; set; }

        public string SubCategory { get; set; }

        public string Languages { get; set; }

        public string Currency { get; set; }

        public string ImageType { get; set; }

        public string Size { get; set; }

        public byte AffType { get; set; }

        public byte DeviceType { get; set; }
    }
}
