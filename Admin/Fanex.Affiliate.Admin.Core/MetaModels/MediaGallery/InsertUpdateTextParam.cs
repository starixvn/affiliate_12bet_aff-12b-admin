namespace Fanex.Affiliate.Admin.Core.MetaModels.MediaGallery
{
    public class InsertUpdateTextParam: UserInfoParam
    {
        public int TextId { get; set; }

        public string TextContent { get; set; }

        public string Category { get; set; }

        public string Language { get; set; }

        public string Currency { get; set; }

        public byte DeviceType { get; set; }
    }
}
