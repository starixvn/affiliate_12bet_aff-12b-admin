using System.ComponentModel.DataAnnotations;

namespace Fanex.Affiliate.Admin.Core.MetaModels.LandingPage
{
    public class MediaPickerParam
    {
        [Required]
        public int ModuleId { get; set; }

        public string SearchTerm { get; set; }

        public string FileType { get; set; }

        [Range(1, int.MaxValue)]
        public int PageIndex { get; set; } = 1;

        [Range(1, 100)]
        public int PageSize { get; set; } = 20;
    }

    public class AssignMediaToModuleParam : UserInfoParam
    {
        [Required]
        public int PageId { get; set; }

        [Required]
        public int ModuleId { get; set; }

        [Required]
        public int MediaId { get; set; }

        public string MediaRole { get; set; } // e.g., "primary", "secondary", "background"

        public int DisplayOrder { get; set; } = 1;

        public bool IsActive { get; set; } = true;
    }

    public class RemoveMediaFromModuleParam : UserInfoParam
    {
        [Required]
        public int PageId { get; set; }

        [Required]
        public int ModuleId { get; set; }

        [Required]
        public int MediaId { get; set; }
    }

    public class GetModuleAssignedMediaParam
    {
        [Required]
        public int ModuleId { get; set; }

        [Required]
        public int PageId { get; set; }
    }
}
