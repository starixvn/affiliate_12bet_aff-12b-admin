using System;

namespace Fanex.Affiliate.User.Core.MetalModels
{
    [Serializable]
    public class MemberTransactionParam
    {
        public string UserName { get; set; }

        public DateTime? Date { get; set; }

        public bool IsDeposit { get; set; }
    }
}
