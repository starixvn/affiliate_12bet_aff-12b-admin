using System;

namespace Fanex.Affiliate.User.Core.DTO
{
    [Serializable]
    public class VerificationSession
    {
        public int AffId { get; set; }

        public int Counter { get; set; }

        public string Captcha { get; set; }
    }
}
