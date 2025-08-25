using Fanex.Affiliate.Admin.Core.Constants;
using System.ComponentModel.DataAnnotations;

namespace Fanex.Affiliate.Admin.Core.MetaModels.LandingPage
{
    public class UpdateModuleMediaParam : UserInfoParam
    {
        [Required(ErrorMessage = "Media ID is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Invalid Media ID")]
        public int MediaId { get; set; }

        [Required(ErrorMessage = "Media Name is required")]
        [MaxLength(LandingPageConstants.ValidationLimits.StandardTextLength, ErrorMessage = "Media Name must not exceed 200 characters")]
        public string MediaName { get; set; }

        [MaxLength(20, ErrorMessage = "Size must not exceed 20 characters")]
        public string Size { get; set; }

        public string Category { get; set; }

        public string Tags { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
