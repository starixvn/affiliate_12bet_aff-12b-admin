namespace Fanex.Affiliate.Admin.Core.Services.CampaignManagement
{
    using System.Collections.Generic;
    using DTO.CampaignManagement;

    public interface ICampaignService
    {
        CampaignDashboard GetCampaignDashboard();

        IEnumerable<CampaignStatus> GetAllCampaignStatus();
    }
}
