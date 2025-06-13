namespace Fanex.Affiliate.Admin.Core.MetaModels.Operation
{
    public class CpaTierSettingParam
    {
        public int GroupProductId { get; set; }

        public int TierId { get; set; }

        public decimal MinFdm { get; set; }

        public decimal MinTotalDeposit { get; set; }

        public decimal MinWagering { get; set; }

        public decimal CpaAmount { get; set; }
    }
}
