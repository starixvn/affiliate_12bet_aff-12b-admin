using System;

namespace Fanex.Affiliate.Admin.Core.DTO.MediaGallery
{
    public class OfferAff
    {
        public int AffId { get; set; }

        public int PageId { get; set; }

        public string UserName { get; set; }

        public string FileName { get; set; }

        public DateTime EffectiveFromDate { get; set; }

        public DateTime EffectiveToDate { get; set; }
    }
}
