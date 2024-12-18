
using Fanex.Data;

namespace Fanex.Affiliate.Admin.Core.Criterias.UserPanel.LandingPageStatistics
{
    public class LandingPageStatisticsDetailCriteria : BaseLandingPageStatisticsCritera
    {
        const string SP_NAME = "Aff_ClickStatisticsDetailBySite";

        public override string GetSettingKey() => SP_NAME;

        public override bool IsValid() => AffId > 0;

        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        [SpParam(SqlParamName = "NRows")]
        public int TotalItem { get; set; }
    }
}
