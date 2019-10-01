using System.ComponentModel.DataAnnotations;

namespace Fanex.Affiliate.Admin.Core.MetaModels.MediaGallery
{
    public class InsertUpdateBannerParam: UserInfoParam
    {
        public int BannerId { get; set; }

        [Required]
        public string FileName { get; set; }

        public string Category { get; set; }

        public string SubCategory { get; set; }

        public string Language { get; set; }

        public string Currency { get; set; }

        public string Size { get; set; }

        public string ImageType { get; set; }

        public byte AffType { get; set; }

        public byte DeviceType { get; set; }
    }
}
