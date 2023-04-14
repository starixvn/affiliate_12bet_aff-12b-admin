
using Fanex.Affiliate.Admin.Core.Constants;

namespace Fanex.Affiliate.Admin.Core.DTO.Email
{
    public class EmailBase
    {
        public string RootUrl { get; set; }

        public string UserSiteUrl { get; set; }

        public string MailContact { get; set; }

        public string Language { get; set; }

        public string MailTo { get; set; }

        public bool IsHtml { get; set; }

        public string ContactInfo { get; set; }

        public string ImageRootUrl => string.Format(TemplateString.EmailImageRootPath, RootUrl);
    }
}