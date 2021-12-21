using System;
using System.ComponentModel.DataAnnotations;

namespace Fanex.Affiliate.Admin.Core.MetaModels.MediaGallery
{
    public class InsertHtmlFileParam : UserInfoParam
    {
        [Required]
        public string FileName { get; set; }

        public int FileSize { get; set; }

        [Required]
        public DateTime EffectiveFrom { get; set; }

        [Required]
        public DateTime EffectiveTo { get; set; }

        [Required]
        public string Language { get; set; }
    }
}
