using System.Collections.Generic;
using Fanex.Affiliate.User.Core.DTO;

namespace Fanex.Affiliate.User.Core.ViewModels
{
    public class MemberTransactionViewModel
    {
        public IEnumerable<Transaction> TransactionList { get; set; }

        public decimal Total { get; set; }
    }
}
