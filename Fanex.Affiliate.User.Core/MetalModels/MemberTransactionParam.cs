using System;

namespace Fanex.Affiliate.User.Core.MetalModels
{
    public class MemberTransactionParam
    {
        public string UserName { get; set; }

        public DateTime? Date { get; set; }

        public bool IsDeposit { get; set; }
    }
}
