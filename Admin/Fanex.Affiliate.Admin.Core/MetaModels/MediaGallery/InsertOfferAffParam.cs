
using System.ComponentModel.DataAnnotations;

namespace Fanex.Affiliate.Admin.Core.MetaModels.MediaGallery
{
    public class InsertOfferAffParam : UserInfoParam
    {
        [Required(ErrorMessage = "Please enter Affiliate(s)!")]
        public string AffNameList { get; set; }

        public int PageId { get; set; }
    }
}
