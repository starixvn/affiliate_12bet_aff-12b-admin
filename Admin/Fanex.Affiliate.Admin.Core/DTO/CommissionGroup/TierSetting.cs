namespace Fanex.Affiliate.Admin.Core.DTO.CommissionGroup
{
    public class TierSetting
    {
        public int GroupId { get; set; }

        public int TierId { get; set; }

        public decimal MinAmount { get; set; }

        public decimal MaxAmount { get; set; }

        public int MinQm { get; set; }

        public int MaxQm { get; set; }

        public decimal Commission { get; set; }
    }
}
