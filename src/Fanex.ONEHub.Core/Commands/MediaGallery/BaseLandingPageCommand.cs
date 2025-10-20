namespace Fanex.Affiliate.Admin.Core.Commands.MediaGallery
{
    public abstract class BaseLandingPageCommand : BaseNonQueryCommand
    {
        public string PageCode { get; set; }

        public string PageName { get; set; }

        public string BlockTargetCountry { get; set; }

        public bool IsMobile { get; set; }

        public bool IsExcluded { get; set; }

        public string DealIds { get; set; }
    }
}
