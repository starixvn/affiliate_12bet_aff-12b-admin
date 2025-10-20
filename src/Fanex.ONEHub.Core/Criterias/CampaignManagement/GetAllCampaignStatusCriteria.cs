namespace Fanex.Affiliate.Admin.Core.Criterias.CampaignManagement
{
    using Data.Repository;

    public class GetAllCampaignStatusCriteria : CriteriaBase
    {
        public override string GetSettingKey() => "Aff_LandingPage_Campaign_Status_Sel";

        public override bool IsValid() => true;
    }
}
