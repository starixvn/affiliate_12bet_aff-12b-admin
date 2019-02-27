using System.Collections.Generic;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Affiliate;
using Fanex.Affiliate.Admin.Core.DTO.CommissionGroup;

namespace Fanex.Affiliate.Admin.Core.ViewModels
{
    public class AssignPrivateGroupViewModel
    {
        public IEnumerable<Product> ProductList { get; set; }

        public IEnumerable<Deduction> DeductionList { get; set; }

        public AffiliateBasicInfo AffInfo { get; set; }
    }
}
