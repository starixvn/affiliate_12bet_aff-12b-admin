using System;

namespace Fanex.Affiliate.User.Core.DTO.UserPanel
{

    [Serializable]
    public class AffiliateCommission
    {
        public DateTime MonthDate { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal TurnOver { get; set; }
        public decimal WinLoss { get; set; }
        public decimal PlatformFee { get; set; }
        public decimal OperationalFee { get; set; }
        public decimal BankingFee { get; set; }
        public decimal Bonus { get; set; }
        public decimal CommissionRate { get; set; }
        public decimal Commission { get; set; }
        public int CalculatedType { get; set; }
       
    }
}
