namespace Fanex.Affiliate.Admin.Core.ViewModels.MediaGallery
{
    public class ImageSizeViewModel
    {
        public byte TargetProductId { get; set; }

        public string Language { get; set; }

        public byte AffType { get; set; } = Constants.AffType.Asian;
    }
}
