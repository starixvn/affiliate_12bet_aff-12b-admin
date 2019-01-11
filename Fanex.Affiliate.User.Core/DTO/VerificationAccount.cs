using System;

namespace Fanex.Affiliate.User.Core.DTO
{
    [Serializable]
    public class VerificationAccount
    {
        public int AffId { get; set; }

        public int Counter { get; set; }
    }
}
