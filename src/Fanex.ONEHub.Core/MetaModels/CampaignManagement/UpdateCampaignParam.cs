namespace Fanex.Affiliate.Admin.Core.MetaModels.CampaignManagement
{
    public class UpdateCampaignParam : UserInfoParam
    {
        public int CampaignId { get; set; }

        public string CampaignName { get; set; }

        public string Description { get; set; }

        public int StatusId { get; set; }
    }
}