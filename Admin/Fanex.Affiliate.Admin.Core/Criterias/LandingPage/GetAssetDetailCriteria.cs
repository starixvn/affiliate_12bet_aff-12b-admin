using Fanex.Data.Repository;

namespace Fanex.Affiliate.Admin.Core.Criterias.LandingPage
{
    public class GetAssetDetailCriteria : CriteriaBase
    {
        private const string SP_NAME = "Aff_LandingPage_AssetUploader_ManageSel";

        public override string GetSettingKey() => SP_NAME;

        public override bool IsValid() => true;

        public int MediaId { get; set; }
        public int UserId { get; set; }
        public string FunctionName { get; set; }
        public string ModuleIds { get; set; } = string.Empty;
    }
}
