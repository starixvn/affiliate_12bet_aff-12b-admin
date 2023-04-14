using Fanex.Affiliate.User.Core.Constants;
using System.Diagnostics.CodeAnalysis;

namespace Fanex.Affiliate.User.Core.ViewModels
{
    public class EmailTemplateViewModel
    {
        public string RootUrl { get; set; }

        public string ImageRootUrl => string.Format(TemplateString.EmailImageRootPath, RootUrl);

        public string Language { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }
    }
}
