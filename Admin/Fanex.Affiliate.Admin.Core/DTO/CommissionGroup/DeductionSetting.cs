using Fanex.Affiliate.Admin.Core.Constants;
using System.ComponentModel.DataAnnotations;

namespace Fanex.Affiliate.Admin.Core.DTO.CommissionGroup
{
    public class DeductionSetting
    {
        [Range(Validation.MinId, Validation.MaxId, ErrorMessage = "GroupId not exists!")]
        public int GroupId { get; set; }

        [Range(Validation.MinId, Validation.MaxId, ErrorMessage = "DeductionId not exists!")]
        public int DeductionId { get; set; }

        [Required(ErrorMessage = "Deduction Name is required!")]
        public string DeductionName { get; set; }

        [Required(ErrorMessage = "ProductIds is required!")]
        public string ProductIds { get; set; }

        [Range(0, 1, ErrorMessage = "Deducted Percentage not exists!")]
        public decimal DeductedPercentage { get; set; }

        [Range(0, 1, ErrorMessage = "Next Deducted Percentage not exists!")]
        public decimal NextDeductedPercentage { get; set; }
    }
}
