using Fanex.Data;

namespace Fanex.Affiliate.User.Core.MetalModels
{
    public class ContactUsParam
    {
        public int Mid { get; set; }

        public int Creator { get; set; }

        public string Content { get; set; }

        public string Sender { get; set; }

        public string Affiliate { get; set; }

        public string Email { get; set; }

        public string Subject { get; set; }

        public string Ip { get; set; }

        public string Browser { get; set; }

        public string SessionId { get; set; }

        public int ErrCode { get; set; }

        public string ErrMsg { get; set; }

        [SpParam(Ignored = true)]
        public string Captcha { get; set; }
    }
}
