namespace Fanex.Affiliate.Admin.Core.Commands.MediaGallery
{
    public class DeleteBannerCommand : BaseNonQueryCommand
    {
        private const string SP_NAME = "Aff_Admin_Banner_Del";

        public override string GetSettingKey() => SP_NAME;

        public override bool IsValid() => !string.IsNullOrWhiteSpace(BannerIds);

        public string BannerIds { get; set; }

        public byte TargetId { get; set; }
    }
}
