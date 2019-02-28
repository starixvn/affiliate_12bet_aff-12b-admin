namespace Fanex.Affiliate.User.Core.MetalModels
{
    public class LoginResultParam
    {
        public int AffId { get; set; }

        public string IpAddress { get; set; }

        public string Browser { get; set; }

        public string UserAgent { get; set; }

        public string Language { get; set; }

        public bool IsSuccessful { get; set; }

        public int ErrCode { get; set; }

        public string ErrMsg { get; set; }
    }
}
