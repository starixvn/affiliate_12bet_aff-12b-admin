namespace Fanex.Affiliate.User.Core.MetalModels
{
    public class LoginResultParam
    {
        public int AffId { get; set; }

        public string LoginIp { get; set; }

        public int ServerId { get; set; }

        public string Language { get; set; }

        public bool IsSuccessful { get; set; }

        public string SessionId { get; set; }

        public int ErrorCode { get; set; }

        public string ErrorMessage { get; set; }
    }
}
