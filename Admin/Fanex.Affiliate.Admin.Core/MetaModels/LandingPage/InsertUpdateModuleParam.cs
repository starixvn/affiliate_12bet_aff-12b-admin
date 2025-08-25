using Fanex.Affiliate.Admin.Core.Constants;
using System.ComponentModel.DataAnnotations;

namespace Fanex.Affiliate.Admin.Core.MetaModels.LandingPage
{
    public class InsertUpdateModuleParam : UserInfoParam
    {
        public int ModuleId { get; set; }

        [Required(ErrorMessage = "Module Name is required.")]
        [MaxLength(LandingPageConstants.ValidationLimits.StandardTextLength, ErrorMessage = "Module Name must not exceed 200 characters")]
        public string ModuleName { get; set; }

        [Required(ErrorMessage = "Module Type is required.")]
        [Range(1, LandingPageConstants.ValidationLimits.ModuleTypeId, ErrorMessage = "Invalid Module Type. Must be between 1 and 4.")]
        public int ModuleType { get; set; }

        [MaxLength(LandingPageConstants.ValidationLimits.StandardTextLength, ErrorMessage = "Remark must not exceed 200 characters")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Remark { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Invalid Navigation Target.")]
        public int NavigateTo { get; set; }

        public bool IsMultipleUpload { get; set; }

        public bool IsRequired { get; set; }

        public bool IsUnique { get; set; }
    }
}
