using Fanex.Affiliate.Admin.Core.Constants;
using System.ComponentModel.DataAnnotations;

namespace Fanex.Affiliate.Admin.Core.MetaModels.MediaGallery
{
    public class InsertUpdateLandingPageParam : UserInfoParam
    {
        public int PageId { get; set; }

        [Required(ErrorMessage = "Landing Page Name is required.")]
        [MaxLength(Validation.MaxLengthLandingPageName, ErrorMessage = "Landing Page Name must not exceed 200 characters")]
        public string PageName { get; set; }

        [Required(ErrorMessage = "Landing Page Code is required.")]
        [MaxLength(Validation.MaxLengthLandingPageCode, ErrorMessage = "Landing Page Code must not exceed 100 characters")]
        public string PageCode { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string BlockTargetCountry { get; set; }

        public bool IsMobile { get; set; }

        public bool IsExcluded { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string DealIds { get; set; }
    }
}
