using System.ComponentModel.DataAnnotations;

namespace Fanex.Affiliate.Admin.Core.MetaModels.MediaGallery
{
    public class InsertUpdateBannerParam: UserInfoParam
    {
        public int BannerId { get; set; }

        public string FilePath { get; set; }

        public string InputFileName { get; set; }

        [Required]
        public string Category { get; set; }

        public string SubCategory { get; set; }

        [Required]
        public string Language { get; set; }

        [Required]
        public string Currency { get; set; }

        public string Size { get; set; }

        [Required]
        public string ImageType { get; set; }

        public byte AffType { get; set; }

        public byte DeviceType { get; set; }
    }
}
