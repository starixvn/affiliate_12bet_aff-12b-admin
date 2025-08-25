using Fanex.Affiliate.Admin.Core.Constants;
using System.ComponentModel.DataAnnotations;

namespace Fanex.Affiliate.Admin.Core.MetaModels.LandingPage
{
    public class UploadModuleMediaParam : UserInfoParam
    {
        [Required(ErrorMessage = "Module ID is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Invalid Module ID")]
        public int ModuleId { get; set; }

        [Range(1, LandingPageConstants.ValidationLimits.ModuleTypeId, ErrorMessage = "Invalid Module Type")]
        public int ModuleType { get; set; }

        [MaxLength(LandingPageConstants.ValidationLimits.StandardTextLength, ErrorMessage = "Media Name must not exceed 200 characters")]
        public string MediaName { get; set; }

        [MaxLength(20, ErrorMessage = "Size must not exceed 20 characters")]
        public string Size { get; set; }

        public bool OverwriteExisting { get; set; }

        public string Category { get; set; }

        public string Tags { get; set; }
    }
}
