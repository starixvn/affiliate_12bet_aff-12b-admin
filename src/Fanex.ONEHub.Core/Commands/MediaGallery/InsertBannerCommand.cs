namespace Fanex.Affiliate.Admin.Core.Commands.MediaGallery
{
    public class InsertBannerCommand : BaseBannerCommand
    {
        private const string SP_NAME = "Aff_Admin_Banner_Ins";

        public override string GetSettingKey() => SP_NAME;

        public override bool IsValid() => true;
    }
}
