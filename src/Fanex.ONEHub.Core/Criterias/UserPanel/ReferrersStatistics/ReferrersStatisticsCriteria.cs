
namespace Fanex.Affiliate.Admin.Core.Criterias.UserPanel.ReferrersStatistics
{
    public class ReferrersStatisticsCriteria : BaseReferrersStatisticsCriteria
    {
        const string SP_NAME = "Aff_ClickStatisticsSelBySite";

        public override string GetSettingKey() => SP_NAME;

        public override bool IsValid() => AffId > 0;
    }
}
