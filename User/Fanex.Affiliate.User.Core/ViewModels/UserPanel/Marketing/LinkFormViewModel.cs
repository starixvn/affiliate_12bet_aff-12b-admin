using Fanex.Affiliate.User.Core.DTO.UserPanel.MediaGallery;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Fanex.Affiliate.User.Core.ViewModels.UserPanel.Marketing
{
    public class LinkFormViewModel
    {
        public string SelectedTargetCountry { get; set; }

        public string SelectedLanguage { get; set; }

        public Domain SelectedDomain { get; set; }

        public IEnumerable<SelectListItem> TargetCountryList { get; set; }

        public IEnumerable<SelectListItem> LanguageList { get; set; }

        public IEnumerable<SelectListItem> DomainList { get; set; }

        public IEnumerable<SelectListItem> MemberSitePageList { get; set; }
    }
}
