using System.ComponentModel.DataAnnotations;

namespace Fanex.Affiliate.Admin.Core.MetaModels.MediaGallery
{
    public class DeleteLandingPageParam
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "Please select the items to delete")]
        public string PageIds { get; set; }

        public string Ip { get; set; }
    }
}
