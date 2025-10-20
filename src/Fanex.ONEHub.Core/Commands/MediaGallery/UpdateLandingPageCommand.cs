namespace Fanex.Affiliate.Admin.Core.Commands.MediaGallery
{
    public class UpdateLandingPageCommand : BaseLandingPageCommand
    {
        private const string SP_NAME = "Aff_Admin_LandingPage_Upd";

        public override string GetSettingKey() => SP_NAME;

        public override bool IsValid() => PageId > 0;

        public int PageId { get; set; }
    }
}
