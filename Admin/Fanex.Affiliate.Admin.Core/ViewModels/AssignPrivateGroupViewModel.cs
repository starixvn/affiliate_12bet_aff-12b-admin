using Fanex.Affiliate.Admin.Core.DTO.Common;
using Fanex.Affiliate.Admin.Core.DTO.Affiliate;
using Fanex.Affiliate.Admin.Core.DTO.CommissionGroup;
using System.Collections.Generic;

namespace Fanex.Affiliate.Admin.Core.ViewModels
{
    public class AssignPrivateGroupViewModel
    {
        public IEnumerable<Product> ProductList { get; set; }

        public IEnumerable<Deduction> DeductionList { get; set; }

        public AffiliateBasicInfo AffInfo { get; set; }
    }
}
