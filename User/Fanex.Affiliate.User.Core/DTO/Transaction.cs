using System;

namespace Fanex.Affiliate.User.Core.DTO
{
    public class Transaction
    {
        public string TransactionId { get; set; }

        public string OriginalAmount { get; set; }

        public string OriginalTransactionDate { get; set; }

        public string Status { get; set; }

        public string AccountNo { get; set; }

        public string NameOfBank { get; set; }

        public string BankAddress { get; set; }

        public string BankCountry { get; set; }

        public string BankCityTown { get; set; }

        public string Currency { get; set; }

        public decimal Amount => Convert.ToDecimal(OriginalAmount);
    }
}
