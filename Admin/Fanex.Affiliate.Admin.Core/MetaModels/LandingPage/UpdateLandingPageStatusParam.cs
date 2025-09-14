using Fanex.Affiliate.Admin.Core.Constants;
using System.ComponentModel.DataAnnotations;

namespace Fanex.Affiliate.Admin.Core.MetaModels.LandingPage
{
    public class UpdateLandingPageStatusParam : UserInfoParam
    {
        [Required]
        public int PageId { get; set; }

        [Range(0, LandingPageConstants.AssetStatusTypes.Inactive, ErrorMessage = "Status is invalid")]
        public byte StatusId { get; set; }
    }
}
