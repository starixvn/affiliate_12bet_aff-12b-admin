using Fanex.Affiliate.Admin.Core.Constants;
using System.ComponentModel.DataAnnotations;

namespace Fanex.Affiliate.Admin.Core.MetaModels.LandingPage
{
    public class InsertLandingPageCampaignParam : UserInfoParam
    {
        [Required]
        public string CampaignName { get; set; }

        public string Description { get; set; }

        public int StatusId { get; set; } = LandingPageConstants.CampaignStatusTypes.Active;
    }
}
