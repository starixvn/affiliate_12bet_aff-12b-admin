using System.Collections.Generic;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.CommissionGroup;

namespace Fanex.Affiliate.Admin.Core.ViewModels.Affiliate
{
    public class AffiliateInfoViewModel
    {
        public IEnumerable<SelectItem> CommGroupList { get; set; }

        public IEnumerable<Language> LanguageList { get; set; }
    }
}
