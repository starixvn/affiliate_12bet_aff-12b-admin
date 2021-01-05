using Fanex.Affiliate.User.Core.DTO;
using System.Collections.Generic;

namespace Fanex.Affiliate.User.Core.ViewModels
{
    public class MemberTransactionViewModel
    {
        public IEnumerable<Transaction> TransactionList { get; set; }

        public decimal Total { get; set; }
    }
}
