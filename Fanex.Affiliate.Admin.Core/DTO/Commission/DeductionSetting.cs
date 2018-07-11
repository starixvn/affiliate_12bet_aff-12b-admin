namespace Fanex.Affiliate.Admin.Core.DTO.Commission
{
    public class DeductionSetting
    {
        public int GroupId { get; set; }

        public int DeductionId { get; set; }

        public string DeductionName { get; set; }

        public string ProductIds { get; set; }

        public decimal DeductedPercentage { get; set; }

        public decimal NextDeductedPercentage { get; set; }
    }
}
