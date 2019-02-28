using System.Collections.Generic;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Affiliate;

namespace Fanex.Affiliate.Admin.Core.ViewModels
{
    public class AssignNormalGroupViewModel
    {
        public IEnumerable<SelectItem> CommGroupList { get; set; }

        public int SelectedGroup { get; set; }

        public AffiliateBasicInfo AffInfo { get; set; }
    }
}
