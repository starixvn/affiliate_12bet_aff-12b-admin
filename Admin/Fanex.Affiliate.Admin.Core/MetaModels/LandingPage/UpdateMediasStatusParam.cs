using Fanex.Affiliate.Admin.Core.Constants;
using System.ComponentModel.DataAnnotations;

namespace Fanex.Affiliate.Admin.Core.MetaModels.LandingPage
{
    public class UpdateMediasStatusParam : UserInfoParam
    {
        [Required]
        public string MediaIds { get; set; }

        public string FunctionName { get; set; }

        [Range(LandingPageConstants.AssetStatusTypes.Active, LandingPageConstants.AssetStatusTypes.Inactive, ErrorMessage = "Status is invalid")]
        public byte Status { get; set; }
    }
}
