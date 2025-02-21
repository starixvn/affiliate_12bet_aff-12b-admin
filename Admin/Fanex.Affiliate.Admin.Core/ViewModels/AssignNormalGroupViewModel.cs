using Fanex.Affiliate.Admin.Core.DTO.Affiliate;
using Fanex.Affiliate.Admin.Core.DTO.CommissionGroup;
using System.Collections.Generic;

namespace Fanex.Affiliate.Admin.Core.ViewModels
{
    public class AssignNormalGroupViewModel
    {
        public IEnumerable<NormalCommGroup> CommGroupList { get; set; }

        public IEnumerable<CommGroupType> CommGroupTypes { get; set; }

        public int SelectedGroupType { get; set; }

        public int SelectedGroup { get; set; }

        public AffiliateBasicInfo AffInfo { get; set; }
    }
}
