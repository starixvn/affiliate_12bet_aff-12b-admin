
namespace Fanex.Affiliate.Admin.Core.MetaModels.MediaGallery
{
    public class SizeParam
    {
        public byte TargetProductId { get; set; }

        public string Languages { get; set; }

        public byte AffType { get; set; } = Constants.AffType.Asian;
    }
}
