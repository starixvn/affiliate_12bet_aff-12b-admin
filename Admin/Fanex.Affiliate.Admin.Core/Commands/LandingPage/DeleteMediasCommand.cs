namespace Fanex.Affiliate.Admin.Core.Commands.LandingPage
{
    public class DeleteMediasCommand : BaseMediaNonQueryCommand
    {
        private const string SP_NAME = "Aff_LandingPage_AssetUploader_Del";

        public override string GetSettingKey() => SP_NAME;

        public override bool IsValid() => !string.IsNullOrWhiteSpace(MediaIds);

        public string MediaIds { get; set; }
    }
}
