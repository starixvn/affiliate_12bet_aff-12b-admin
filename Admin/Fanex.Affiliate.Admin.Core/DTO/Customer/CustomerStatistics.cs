using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;

namespace Fanex.Affiliate.Admin.Core.DTO.Customer
{
    public class CustomerStatistics
    {
        public int NewSignUpByMonth { get; set; }

        public int NewFdmByMonth { get; set; }

        public int NewSignUpTotal { get; set; }

        public int NewFdmTotal { get; set; }

        [PermissionCheck(Function.AffPlayerDetail)]
        public int ActivePlayerByMonth { get; set; }

        [PermissionCheck(Function.AffPlayerDetail)]
        public int QualifiedMemberByMonth { get; set; }

        [PermissionCheck(Function.AffPlayerDetail)]
        public decimal DepositByMonth { get; set; }

        [PermissionCheck(Function.AffPlayerDetail)]
        public decimal DepositTotal { get; set; }

        [PermissionCheck(Function.AffPlayerDetail)]
        public decimal FdmDepositByMonth { get; set; }

        [PermissionCheck(Function.Turnover)]
        public decimal TurnoverByMonth { get; set; }

        [PermissionCheck(Function.Turnover)]
        public decimal TurnoverTotal { get; set; }

        public decimal WinlostByMonth { get; set; }

        public decimal WinlostTotal { get; set; }


        public int UniqueClickPcLaptopByMonth { get; set; }

        public int TotalClickPcLaptopByMonth { get; set; }

        public int UniqueClickMobileByMonth { get; set; }

        public int TotalClickMobileByMonth { get; set; }
    }
}
