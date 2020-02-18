using System.ComponentModel.DataAnnotations;

namespace Fanex.Affiliate.Admin.Core.MetaModels.MediaGallery
{
    public class InsertMultipleBannersParam : UserInfoParam
    {
        public int BannerId { get; set; }

        public string FilePath { get; set; }

        public string InputFileNames { get; set; }

        [Required]
        public string Categories { get; set; }

        public string SubCategories { get; set; }

        [Required]
        public string Language { get; set; }

        [Required]
        public string Currencies { get; set; }

        public string Size { get; set; }

        [Required]
        public string ImageTypes { get; set; }

        [Required]
        public string AffTypes { get; set; }

        [Required]
        public string DeviceTypes { get; set; }
    }
}
