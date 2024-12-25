namespace Fanex.Affiliate.Admin.Core.Commands.MediaGallery
{
    public class UpdateBannerCommand : BaseBannerCommand
    {
        private const string SP_NAME = "Aff_Admin_Banner_Upd";

        public override string GetSettingKey() => SP_NAME;

        public override bool IsValid() => BannerId > 0;

        public int BannerId { get; set; }
    }
}
