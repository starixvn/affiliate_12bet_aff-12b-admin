using Fanex.Affiliate.Admin.Core.Constants;
using System.ComponentModel.DataAnnotations;

namespace Fanex.Affiliate.Admin.Core.DTO.CommissionGroup
{
    public class DeductionSetting
    {
        public int GroupId { get; set; }

        [Range(Validation.MinId, Validation.MaxId, ErrorMessage = "DeductionId not exists!")]
        public int DeductionId { get; set; }

        public string DeductionName { get; set; }

        [Required(ErrorMessage = "ProductIds is required!")]
        public string ProductIds { get; set; }

        [Range(0, 1, ErrorMessage = "Deducted Percentage is incorrect!")]
        public decimal DeductedPercentage { get; set; }

        [Range(0, 1, ErrorMessage = "Next Deducted Percentage is incorrect!")]
        public decimal NextDeductedPercentage { get; set; }
    }
}
