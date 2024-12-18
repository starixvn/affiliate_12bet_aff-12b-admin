
namespace Fanex.Affiliate.Admin.Core.Criterias.UserPanel.LandingPageStatistics
{
    public class LandingPageStatisticsCriteria : BaseLandingPageStatisticsCritera
    {
        const string SP_NAME = "Aff_ClickStatisticsSelByPage";

        public override string GetSettingKey() => SP_NAME;

        public override bool IsValid() => AffId > 0;
    }
}
