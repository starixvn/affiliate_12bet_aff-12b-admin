namespace Fanex.Affiliate.Admin.Core.Commands.MediaGallery
{
    public class InsertLandingPageCommand : BaseLandingPageCommand
    {
        private const string SP_NAME = "Aff_Admin_LandingPage_Ins";

        public override string GetSettingKey() => SP_NAME;

        public override bool IsValid() => true;
    }
}
