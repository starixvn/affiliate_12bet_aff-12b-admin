namespace Fanex.Affiliate.Admin.Core.Services.CampaignManagement
{
    using System.Collections.Generic;
    using Criterias.CampaignManagement;
    using Data.Repository;
    using DTO;
    using DTO.Campaign;
    using DTO.CampaignManagement;
    using Extensions;
    using MetaModels.CampaignManagement;

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

        public EntityResult<CampaignDetailInfo> GetCampaignList(GetCampaignListParam param)
        {
            var criteria = param.MapTo<GetCampaignListParam, GetCampaignListCriteria>();
            return FetchEntityResult<CampaignDetailInfo>(criteria);
        }
    }
}
