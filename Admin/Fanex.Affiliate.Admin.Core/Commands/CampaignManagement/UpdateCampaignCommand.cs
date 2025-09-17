namespace Fanex.Affiliate.Admin.Core.Commands.CampaignManagement
{
    public class UpdateCampaignCommand : BaseNonQueryCommand
    {
        public int CampaignId { get; set; }

        public string CampaignName { get; set; }

        public string Description { get; set; }

        public int StatusId { get; set; }

        public override string GetSettingKey() => "Aff_LandingPage_Campaign_Upd";

        public override bool IsValid()
        {
            return StatusId >= 0
                && CampaignId > 0
                && !string.IsNullOrWhiteSpace(CampaignName);
        }
    }
}