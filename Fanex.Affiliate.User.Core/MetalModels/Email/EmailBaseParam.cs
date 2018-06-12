using Fanex.Affiliate.User.Core.DTO;

namespace Fanex.Affiliate.User.Core.MetalModels.Email
{
    public class EmailBaseParam
    {
        public string RootDomain { get; set; }

        public string MailContact { get; set; }

        public string MailTo { get; set; }

        public string MailCc { get; set; } = string.Empty;

        public string MailBcc { get; set; } = string.Empty;

        public UserInfo User { get; set; }
    }
}
