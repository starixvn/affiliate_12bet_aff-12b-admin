using Fanex.Data;
using System.ComponentModel.DataAnnotations;

namespace Fanex.Affiliate.User.Core.MetalModels
{
    public class ContactUsParam
    {
        public int Creator { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public string Sender { get; set; }

        public string Affiliate { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Subject { get; set; }

        public string Ip { get; set; }

        public string Browser { get; set; }

        public string SessionId { get; set; }

        [Required]
        [SpParam(Ignored = true)]
        public string Captcha { get; set; }
    }
}
