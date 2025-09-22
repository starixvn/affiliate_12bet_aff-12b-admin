using Fanex.Affiliate.Admin.Core.Constants;
using System.ComponentModel.DataAnnotations;

namespace Fanex.Affiliate.Admin.Core.MetaModels.MediaGallery
{
    public class InsertUpdateLandingPageParam : UserInfoParam
    {
        public int PageId { get; set; }

        [Required(ErrorMessage = "Landing Page Name is required.")]
        [MaxLength(Validation.MaxLengthInLandingPage, ErrorMessage = "Page name must not exceed 50 characters.")]
        public string PageName { get; set; }

        [Required(ErrorMessage = "Landing Page Code is required.")]
        [MaxLength(Validation.MaxLengthInLandingPage, ErrorMessage = "Page code must not exceed 50 characters, without spaces or special characters.")]
        public string PageCode { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string BlockTargetCountry { get; set; }

        public bool IsMobile { get; set; }

        public bool IsExcluded { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string DealIds { get; set; }
    }
}
