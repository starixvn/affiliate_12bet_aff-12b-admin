using Fanex.Data;

namespace Fanex.Affiliate.Admin.Core.Commands.LandingPage
{
    public class UpdateAssetsStatusCommand : BaseAssetNonQueryCommand
    {
        private const string SP_NAME = "Aff_LandingPage_AssetUploader_Status_Upd";

        public override string GetSettingKey() => SP_NAME;

        public override bool IsValid() => !string.IsNullOrWhiteSpace(MediaIds);

        public string MediaIds { get; set; }

        [SpParam(SqlParamName = "StatusId")]
        public byte Status { get; set; }
    }
}
