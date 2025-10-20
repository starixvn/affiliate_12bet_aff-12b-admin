namespace Fanex.Affiliate.Admin.Core.ViewModels.Affiliate
{
    using System.Collections.Generic;
    using DTO;
    using DTO.Common;
    using DTO.Operation;

    public class MarketingAccountViewModel
    {
        public IEnumerable<Language> LanguageList { get; set; }

        public IEnumerable<TargetCountry> TargetCountryList { get; set; }

        public IEnumerable<SubMenu> SubMenuList { get; set; }

        public IEnumerable<NormalDeal> DealList { get; set; }
    }
}
