using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Fanex.Affiliate.Admin.Core.MetaModels.MediaGallery
{
    public class InsertUpdateMultipleBannersParam : UserInfoParam
    {
        public int BannerId { get; set; }

        public string Files { get; set; }

        public List<string> FilePaths { get; set; } = new List<string>();

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string InputFileNames { get; set; } = string.Empty;

        [Required]
        public string Categories { get; set; }

        [Required]
        public string Language { get; set; }

        public List<string> Sizes { get; set; } = new List<string>();

        [Required]
        public string AffTypes { get; set; }
    }
}
