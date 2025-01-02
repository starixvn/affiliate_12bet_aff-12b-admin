namespace Fanex.Affiliate.Admin.Core.Commands.MediaGallery
{
    public class DeleteLandingPageCommand : BaseNonQueryCommand
    {
        private const string SP_NAME = "Aff_Admin_LandingPage_Del";

        public override string GetSettingKey() => SP_NAME;

        public override bool IsValid() => !string.IsNullOrWhiteSpace(PageIds);

        public string PageIds { get; set; }
    }
}
