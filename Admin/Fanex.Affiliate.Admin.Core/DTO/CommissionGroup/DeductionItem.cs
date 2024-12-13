namespace Fanex.Affiliate.Admin.Core.DTO.CommissionGroup
{
    public class DeductionItem
    {
        public virtual int DeductionId { get; set; }

        public virtual string DeductionName { get; set; }

        public decimal DeductedPercentage { get; set; }

        public bool IsDeductedPercent { get; set; }

        public string DeductedAmount { get; set; }

        public decimal Commission { get; set; }

        public int GroupProductId { get; set; }
    }
}
