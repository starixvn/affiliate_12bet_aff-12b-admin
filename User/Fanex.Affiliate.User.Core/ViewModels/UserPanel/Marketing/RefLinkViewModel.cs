namespace Fanex.Affiliate.User.Core.ViewModels.UserPanel.Marketing
{
    using DTO.UserPanel.MediaGallery;
    using Enum;
    using System.Collections.Generic;

    public class RefLinkViewModel
    {
        public AffiliateLinkType AffLinkType { get; set; }

        public string AffTypeDisplayName { get; set; }

        public IEnumerable<Domain> Domains { get; set; }

        public IEnumerable<LandingPage> SitePages { get; set; }

    }
}