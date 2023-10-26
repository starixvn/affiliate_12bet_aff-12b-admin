using Fanex.Affiliate.User.Core.DTO.UserPanel.MediaGallery;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Fanex.Affiliate.User.Core.ViewModels.UserPanel.Marketing
{
    public class LinkFormViewModel
    {
        public IEnumerable<SelectListItem> TargetCountryList { get; set; }

        public IEnumerable<SelectListItem> LanguageList { get; set; }

        public IEnumerable<Domain> DomainList { get; set; }

        public IEnumerable<SelectListItem> MemberSitePageList { get; set; }
    }
}
