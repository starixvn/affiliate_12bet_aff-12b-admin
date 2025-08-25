using System.ComponentModel.DataAnnotations;

namespace Fanex.Affiliate.Admin.Core.MetaModels.LandingPage
{
    public class DeleteModuleMediaParam : UserInfoParam
    {
        [Required(ErrorMessage = "Please select media files to delete")]
        public string MediaIds { get; set; }

        public bool PermanentDelete { get; set; }

        public string DeleteReason { get; set; }
    }
}
