using Fanex.Affiliate.Admin.Core.DTO;
using System.Collections.Generic;

namespace Fanex.Affiliate.Admin.Core.ViewModels.UserPanel
{
    public class LandingPagesViewModel
    {
        public IEnumerable<SubMenu> SubMenuList { get; set; }

        public IEnumerable<Language> LanguageList { get; set; }
    }
}
