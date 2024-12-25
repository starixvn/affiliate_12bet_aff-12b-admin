namespace Fanex.Affiliate.Admin.Core.Commands.MediaGallery
{
    public abstract class BaseBannerCommand : BaseNonQueryCommand
    {
        public byte TargetId { get; set; }

        public string Files { get; set; }

        public string Categories { get; set; }

        public string SubCategories { get; set; }

        public string Language { get; set; }

        public string Currencies { get; set; }

        public string AffTypes { get; set; }

        public string DeviceTypes { get; set; }
    }
}
