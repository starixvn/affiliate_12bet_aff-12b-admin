using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Fanex.Affiliate.Admin.Core.MetaModels.LandingPage
{
    public class UploadAssetsParam : UserInfoParam
    {
        [Required(ErrorMessage = "Module Id is required")]
        public int ModuleId { get; set; }

        [Required(ErrorMessage = "Target Country is required")]
        public string TargetCountryCode { get; set; }

        [Required(ErrorMessage = "Language is required")]
        public string MarketId { get; set; }

        public string Files { get; set; }

        public List<string> FilePaths { get; } = new List<string>();

        public List<string> MediaNames { get; } = new List<string>();

        public List<string> Dimensions { get; } = new List<string>();

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string InputFileNames { get; set; } = string.Empty;

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string InputMediaNames { get; set; } = string.Empty;

        public string FunctionName { get; set; }
    }
}
