namespace Fanex.Affiliate.Admin.Core.Commands.LandingPage
{
    public class UploadAssetsCommand : BaseAssetNonQueryCommand
    {
        private const string SP_NAME = "Aff_LandingPage_AssetUploader_Ins";

        public string Files { get; set; }
        public int ModuleId { get; set; }
        public string TargetCountryCode { get; set; }
        public string MarketId { get; set; }
        public override string GetSettingKey() => SP_NAME;

        public override bool IsValid() => 
            ModuleId > 0 && 
            !string.IsNullOrWhiteSpace(Files);
    }
}
