namespace Fanex.Affiliate.Admin.Core.DTO.MediaGallery
{
    public class LandingPage
    {
        public int PageId { get; set; }

        public string PageName { get; set; }

        public string PageCode { get; set; }

        public bool IsExcluded { get; set; }

        public string DealIds { get; set; }

        public bool IsMobile { get; set; }

        public string BlockTargetCountry { get; set; }

        public bool CanEdit { get; set; }

        public bool IsDefault { get; set; }

        public byte StatusId { get; set; }
    }
}
