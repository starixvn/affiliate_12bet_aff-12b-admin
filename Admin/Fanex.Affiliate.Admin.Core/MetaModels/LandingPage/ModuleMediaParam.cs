using Fanex.Affiliate.Admin.Core.Constants;
using System.ComponentModel.DataAnnotations;

namespace Fanex.Affiliate.Admin.Core.MetaModels.LandingPage
{
    public class ModuleMediaParam
    {
        [Required(ErrorMessage = "Module ID is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Invalid Module ID")]
        public int ModuleId { get; set; }

        [MaxLength(Validation.MaxLengthLandingPage, ErrorMessage = "Search term must not exceed 100 characters")]
        public string SearchTerm { get; set; }

        public string FileType { get; set; }

        public string SizeFilter { get; set; }

        public bool IncludeDeleted { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }
    }
}
