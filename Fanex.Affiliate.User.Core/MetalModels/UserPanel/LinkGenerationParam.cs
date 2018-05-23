using System.Collections.Generic;
using Fanex.Affiliate.User.Core.DTO.UserPanel;

namespace Fanex.Affiliate.User.Core.MetalModels.UserPanel
{
    public class LinkGenerationParam : BaseParam
    {
        public int DomainId { get; set; }

        public string SelectedPage { get; set; }

        public string TextLinkTemplate { get; set; }

        public int AffId { get; set; }

        public string SelectedLanguage { get; set; }

        public string PreferredLanguage { get; set; }

        public IEnumerable<BannerType> LanguageList { get; set; }

        public Domain SelectedDomain { get; set; }
    }
}
