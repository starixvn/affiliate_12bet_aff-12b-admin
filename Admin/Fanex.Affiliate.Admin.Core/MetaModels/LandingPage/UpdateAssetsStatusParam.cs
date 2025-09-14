using Fanex.Affiliate.Admin.Core.Constants;
using System.ComponentModel.DataAnnotations;

namespace Fanex.Affiliate.Admin.Core.MetaModels.LandingPage
{
    public class UpdateAssetsStatusParam : BaseAssetParam
    {
        [Required]
        public string MediaIds { get; set; }

        [Range(LandingPageConstants.AssetStatusTypes.Active, LandingPageConstants.AssetStatusTypes.Inactive, ErrorMessage = "Status is invalid")]
        public byte StatusId { get; set; }
    }
}
