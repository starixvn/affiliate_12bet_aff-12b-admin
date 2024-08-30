using Fanex.Affiliate.Admin.Core.DTO.Affiliate;
using Fanex.Affiliate.Admin.Core.DTO.Common;
using System.Collections.Generic;

namespace Fanex.Affiliate.Admin.Core.ViewModels
{
    public class AssignNormalGroupViewModel
    {
        public IEnumerable<SelectItem> CommGroupList { get; set; }

        public int SelectedGroup { get; set; }

        public AffiliateBasicInfo AffInfo { get; set; }

        public bool HasCommGroupVisible { get; set; }
    }
}
