namespace Fanex.Affiliate.Admin.Core.Services.CampaignManagement
{
    using System.Collections.Generic;
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

        public IEnumerable<CampaignStatus> GetAllCampaignStatus()
        {
            return FetchTData<CampaignStatus>(new GetAllCampaignStatusCriteria());
        }
    }
}
