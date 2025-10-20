using Fanex.Data;

namespace Fanex.Affiliate.Admin.Core.Criterias.LandingPage
{
    public class GetAssetUploaderCriteria : PagingCriteria
    {
        private const string SP_NAME = "Aff_LandingPage_AssetUploader_ManageSel";

        public override string GetSettingKey() => SP_NAME;

        public override bool IsValid() => true;

        public int UserId { get; set; }
        [SpParam(SqlParamName = "StatusId")]
        public int Status { get; set; }
        public string ModuleIds { get; set; }
        public string TargetCountrycodes { get; set; }
        [SpParam(SqlParamName = "MarketIds")]
        public string LanguageCodes { get; set; }
        public string FunctionName { get; set; }
        public int MediaId { get; set; }
        public string MediaName { get; set; }
    }
}
