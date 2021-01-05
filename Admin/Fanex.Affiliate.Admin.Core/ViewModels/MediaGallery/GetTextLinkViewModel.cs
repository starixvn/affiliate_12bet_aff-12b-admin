namespace Fanex.Affiliate.Admin.Core.ViewModels.MediaGallery
{
    public class GetTextLinkViewModel
    {
        public string Category { get; set; }

        public string Language { get; set; }

        public string Currency { get; set; }

        public byte DeviceType { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }
    }
}
