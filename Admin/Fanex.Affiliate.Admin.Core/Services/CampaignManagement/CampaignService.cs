namespace Fanex.Affiliate.Admin.Core.Services.CampaignManagement
{
    using Criterias.CampaignManagement;
    using Data.Repository;
    using DTO.CampaignManagement;

    public class CampaignService : BaseDynamicService, ICampaignService
    {
        public CampaignService(IDynamicRepository dynamicRepository) : base(dynamicRepository)
        {
        }

        public CampaignDashboard GetCampaignDashboard()
        {
            return GetTData<CampaignDashboard>(new GetCampaignDashboardCriteria());
        }
    }
}
