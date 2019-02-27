using System.Collections.Generic;
using Fanex.Affiliate.User.Core.DTO.UserPanel;

namespace Fanex.Affiliate.User.Core.ViewModels.UserPanel.Marketing
{
    public class LinkFormViewModel
    {
        public IEnumerable<Domain> DomainList { get; set; }

        public IEnumerable<LandingPage> MemberSitePageList { get; set; }
    }
}
