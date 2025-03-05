namespace Fanex.Affiliate.User.Core.ViewModels.UserPanel.Marketing
{
    using DTO;
    using System.Collections.Generic;

    public class AffiliateLinkViewModel
    {
        public IEnumerable<RefLinkViewModel> RefLinks { get; set; }

        public IEnumerable<Language> Languages { get; set; }

        public IDictionary<string, string> Channels { get; set; }
    }
}
