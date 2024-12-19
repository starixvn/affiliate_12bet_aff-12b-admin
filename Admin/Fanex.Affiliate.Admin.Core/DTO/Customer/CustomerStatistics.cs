namespace Fanex.Affiliate.Admin.Core.DTO.Customer
{
    public class CustomerStatistics
    {
        public int SignupsByMonth { get; set; }

        public int FdmByMonth { get; set; }

        public int SignupsTotal { get; set; }

        public int FdmTotal { get; set; }

        public int ActivePlayersByMonth { get; set; }

        public decimal DepositByMonth { get; set; }

        public decimal DepositTotal { get; set; }

        public decimal TurnoverByMonth { get; set; }

        public decimal TurnoverTotal { get; set; }

        public decimal GgrByMonth { get; set; }

        public decimal GgrTotal { get; set; }

        public int UniqueClickPCByMonth { get; set; }

        public int TotalClickPCByMonth { get; set; }

        public int UniqueClickMobileByMonth { get; set; }

        public int TotalClickMobileByMonth { get; set; }

        public int NoOfSubAffByMonth { get; set; }

        public int NoOfSubAffTotal { get; set; }
    }
}
