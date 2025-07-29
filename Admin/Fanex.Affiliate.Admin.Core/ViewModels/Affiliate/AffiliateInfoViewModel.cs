using System.Collections.Generic;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Common;
using Fanex.Affiliate.Admin.Core.DTO.Operation;
using Fanex.Affiliate.Admin.Core.MetaModels;

namespace Fanex.Affiliate.Admin.Core.ViewModels.Affiliate
{
    public class AffiliateInfoViewModel
    {
        public IEnumerable<NormalDeal> CommGroupList { get; set; }

        public IEnumerable<Language> LanguageList { get; set; }

        public IEnumerable<TargetCountry> TargetCountryList { get; set; }

        public int SelectedGroupId { get; set; }

        public ParamFromHome Param { get; set; }

        public IEnumerable<SubMenu> SubMenuList { get; set; }
    }
}
