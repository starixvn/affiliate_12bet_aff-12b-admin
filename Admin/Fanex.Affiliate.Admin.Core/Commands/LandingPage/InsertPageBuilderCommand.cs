using Fanex.Data;
using Fanex.Data.Repository;

namespace Fanex.Affiliate.Admin.Core.Commands.LandingPage
{
    public class InsertPageBuilderCommand : BaseNonQueryCommand
    {
        private const string SP_NAME = "Aff_LandingPage_Builder_Ins";

        public string PageName { get; set; }
        public string PageCode { get; set; }
        public int CampaignId { get; set; }
        public string ComponentStructure { get; set; }
        public string TargetCountryCode { get; set; }

        [SpParam(SqlParamName = "MarketId")]
        public string LanguageCode { get; set; }
        public string DealIds { get; set; }
        public int StatusId { get; set; }
        public virtual bool IsPreview { get; set; }
        public int PageId { get; set; }

        public override string GetSettingKey() => SP_NAME;

        public override bool IsValid() => 
            CampaignId > 0 && 
            !string.IsNullOrWhiteSpace(PageCode);
    }
}
