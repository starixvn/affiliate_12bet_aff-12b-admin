
namespace Fanex.Affiliate.Admin.Core.DTO.Operation
{
    public class RevenueDeductionSetting
    {
        public int GroupId { get; set; }

        public string ProductIds { get; set; } = string.Empty;

        public int DeductionId { get; set; }

        public string DeductionName { get; set; }

        public decimal DeductedPercentage { get; set; }

        public bool IsDeductedPercent { get; set; }

        public string DeductedAmount { get; set; }

        public decimal Commission { get; set; }

        public int GroupProductId { get; set; }
    }
}
