namespace Fanex.Affiliate.User.Core.ViewModels.UserPanel.Marketing
{
    using System.Collections.Generic;
    using DTO;

    public class AffiliateLinkViewModel
    {
        public IEnumerable<RefLinkViewModel> RefLinks { get; set; }

        public IEnumerable<Language> Languages { get; set; }

        public IDictionary<string, string> Channels { get; set; }
    }
}
