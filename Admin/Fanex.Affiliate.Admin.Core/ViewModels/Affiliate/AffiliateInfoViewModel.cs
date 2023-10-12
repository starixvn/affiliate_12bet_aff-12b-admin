using Fanex.Affiliate.Admin.Core.DTO.Common;
using Fanex.Affiliate.Admin.Core.MetaModels;
using System.Collections.Generic;

namespace Fanex.Affiliate.Admin.Core.ViewModels.Affiliate
{
    public class AffiliateFilterViewModel
    {
        public IEnumerable<SelectItem> CommGroupList { get; set; }

        public IEnumerable<Language> LanguageList { get; set; }

        public IEnumerable<Language> TargetCountryList { get; set; }

        public int SelectedGroupId { get; set; }

        public ParamFromHome Param { get; set; }
    }
}
