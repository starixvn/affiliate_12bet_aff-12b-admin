using System;
using System.ComponentModel.DataAnnotations;

namespace Fanex.Affiliate.Admin.Core.MetaModels.MediaGallery
{
    public class InsertHtmlFileParam : UserInfoParam
    {
        [Required(ErrorMessage = "File name is required")]
        public string FileName { get; set; }

        public int FileSize { get; set; }

        [Required(ErrorMessage = "Effective From is required")]
        public DateTime EffectiveFrom { get; set; }

        [Required(ErrorMessage = "Effective To is required")]
        public DateTime EffectiveTo { get; set; }

        [Required(ErrorMessage = "Language cannot be empty")]
        public string Language { get; set; }
    }
}
