namespace Fanex.Affiliate.Admin.Core.MetaModels.CampaignManagement
{
    public class InsertCampaignParam : UserInfoParam
    {
        public string CampaignName { get; set; }

        public string Description { get; set; }

        public int StatusId { get; set; }
    }
}