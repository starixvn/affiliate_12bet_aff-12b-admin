using Fanex.Data;

namespace Fanex.Affiliate.Admin.Core.Criterias.UserPanel.ReferrersStatistics
{
    public class ReferrersStatisticsDetailCriteria : BaseReferrersStatisticsCriteria
    {
        const string SP_NAME = "Aff_ClickStatisticsDetailBySite";

        public override string GetSettingKey() => SP_NAME;

        public override bool IsValid() => AffId > 0;

        public string Domain { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        [SpParam(SqlParamName = "NRows")]
        public int TotalItem { get; set; }
    }
}
