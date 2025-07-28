using Fanex.Affiliate.Admin.Core.DTO.CommissionGroup;

namespace Fanex.Affiliate.Admin.Core.ViewModels.Affiliate
{
    using System.Collections.Generic;
    using DTO;
    using DTO.Common;

    public class MarketingAccountViewModel
    {
        public IEnumerable<Language> LanguageList { get; set; }

        public IEnumerable<TargetCountry> TargetCountryList { get; set; }

        public IEnumerable<SubMenu> SubMenuList { get; set; }

        public IEnumerable<NormalCommGroup> DealList { get; set; }
    }
}
