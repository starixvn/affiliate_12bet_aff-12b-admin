using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Fanex.Affiliate.Admin.Core.MetaModels.MediaGallery
{
    public class InsertUpdateMultipleBannersParam : UserInfoParam
    {
        public int BannerId { get; set; }

        public string Files { get; set; }

        public List<string> FilePaths { get; set; }

        public string InputFileNames { get; set; }

        [Required]
        public string Categories { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string SubCategories { get; set; }

        [Required]
        public string Language { get; set; }

        [Required]
        public string Currencies { get; set; }

        public List<string> Sizes { get; set; }

        [Required]
        public string AffTypes { get; set; }

        [Required]
        public string DeviceTypes { get; set; }
    }
}
