
namespace Fanex.Affiliate.Admin.Core.MetaModels.MediaGallery
{
    public class BannerParam : AffiliatePagingByDateParam
    {
        public byte TargetProductId { get; set; }

        public string Languages { get; set; }

        public string Size { get; set; }

        public byte? AffType { get; set; }
    }
}
