using System;
using System.ComponentModel.DataAnnotations;

namespace Fanex.Affiliate.Admin.Core.ViewModels.MediaGallery
{
    public class NewsLetterViewModel
    {
        public int NewsLetterId { get; set; }

        [Required]
        public DateTime FromDate { get; set; }

        [Required]
        public DateTime ToDate { get; set; }

        [Required]
        public string Language { get; set; }

        [Required]
        public string FileName { get; set; }

        public DateTime? UploadDate { get; set; }

        public string LanguageName { get; set; }

        public int FileSize { get; set; }
    }
}
