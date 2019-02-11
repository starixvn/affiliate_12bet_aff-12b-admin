namespace Fanex.Affiliate.User.Core.MetalModels
{
    public class VerificationOtpParam
    {
        public string AffId { get; set; }

        public int Counter { get; set; }

        public string OtpCode { get; set; }

        public string Captcha { get; set; }

        public string SecretKey { get; set; }

        public string HashMessage { get; set; }
    }
}
