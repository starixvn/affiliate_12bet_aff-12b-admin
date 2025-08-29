using Fanex.Data;

namespace Fanex.Affiliate.Admin.Core.Criterias.LandingPage
{
    public class GetAssetListCriteria : PagingCriteria
    {
        private const string SP_NAME = "Aff_LandingPage_AssetUploader_ManageSel";

        public override string GetSettingKey() => SP_NAME;

        public override bool IsValid() => true;

        public string ModuleIds { get; set; }
        public string TargetCountrycodes { get; set; }
        [SpParam(SqlParamName = "MarketIds")]
        public string LanguageCodes { get; set; }
    }
}
