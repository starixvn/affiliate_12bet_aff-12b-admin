namespace Fanex.Affiliate.Admin.Core.Services.CampaignManagement
{
    using System.Collections.Generic;
    using DTO;
    using DTO.Campaign;
    using DTO.CampaignManagement;
    using MetaModels.CampaignManagement;

    public interface ICampaignService
    {
        CampaignDashboard GetCampaignDashboard();

        IEnumerable<CampaignStatus> GetAllCampaignStatus();

        EntityResult<CampaignDetailInfo> GetCampaignList(GetCampaignListParam param);
    }
}
