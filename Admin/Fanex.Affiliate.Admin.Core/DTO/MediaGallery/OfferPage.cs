using System;

namespace Fanex.Affiliate.Admin.Core.DTO.MediaGallery
{
    public class OfferPage
    {
        public int PageId { get; set; }

        public string FileName { get; set; }

        public int FileSize { get; set; }

        public DateTime UploadDate { get; set; }

        public DateTime EffectiveFromDate { get; set; }

        public DateTime EffectiveToDate { get; set; }

        public string Language { get; set; }

        public int Affiliate { get; set; }
    }
}
