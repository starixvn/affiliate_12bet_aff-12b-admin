using System.Collections.Generic;
using System.Web.Mvc;

namespace Fanex.Affiliate.User.Core.MetalModels.UserPanel
{
    public class LinkGenerationParam : LinkBaseParam
    {
        public int DomainId { get; set; }

        public string SelectedLanguage { get; set; }

        public string PreferredLanguage { get; set; }

        public IEnumerable<SelectListItem> LanguageList { get; set; }
    }
}
