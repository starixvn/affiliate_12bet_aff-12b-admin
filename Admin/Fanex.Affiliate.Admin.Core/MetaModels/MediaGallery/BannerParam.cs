
namespace Fanex.Affiliate.Admin.Core.MetaModels.MediaGallery
{
    public class BannerParam : AffiliatePagingByDateParam
    {
        public string Category { get; set; }

        public string Languages { get; set; }

        public string Size { get; set; }

        public byte AffType { get; set; }
    }
}
