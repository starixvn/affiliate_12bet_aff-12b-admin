namespace Fanex.Affiliate.User.Core.ViewModels.UserPanel.Marketing
{
    using System.Collections.Generic;
    using Enum;
    using DTO.UserPanel.MediaGallery;

    public class RefLinkViewModel
    {
        public AffiliateLinkType AffLinkType { get; set; }

        public string AffTypeDisplayName { get; set; }

        public IEnumerable<Domain> Domains { get; set; }

        public IEnumerable<LandingPage> SitePages { get; set; }

    }
}