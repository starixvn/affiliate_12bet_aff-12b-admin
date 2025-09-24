namespace Fanex.Affiliate.Admin.Core.Criterias.CampaignManagement
{
    using Data.Repository;

    public class GetCampaignDashboardCriteria : CriteriaBase
    {
        public override string GetSettingKey() => "Aff_LandingPage_Campaign_Dashboard_Sel";

        public override bool IsValid() => true;
    }
}
