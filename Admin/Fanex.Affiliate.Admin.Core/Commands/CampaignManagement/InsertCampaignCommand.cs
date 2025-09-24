namespace Fanex.Affiliate.Admin.Core.Commands.CampaignManagement
{
    public class InsertCampaignCommand : BaseNonQueryCommand
    {
        public string CampaignName { get; set; }

        public string Description { get; set; }

        public int StatusId { get; set; }

        public override string GetSettingKey() => "Aff_LandingPage_Campaign_Ins";

        public override bool IsValid()
        {
            return StatusId >= 0 && !string.IsNullOrWhiteSpace(CampaignName);
        }
    }
}
