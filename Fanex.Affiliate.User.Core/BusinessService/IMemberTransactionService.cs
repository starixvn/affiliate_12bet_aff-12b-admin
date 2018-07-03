using System.Collections.Generic;
using Fanex.Affiliate.User.Core.DTO;
using Fanex.Affiliate.User.Core.MetalModels;

namespace Fanex.Affiliate.User.Core.BusinessService
{
    public interface IMemberTransactionService
    {
        IEnumerable<Transaction> GetTransactionInfo(MemberTransactionParam param);
    }
}
