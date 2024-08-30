using Fanex.Affiliate.Admin.Core.DTO.CommissionGroup;
using Fanex.Affiliate.Admin.Core.DTO.Common;
using System.Collections.Generic;

namespace Fanex.Affiliate.Admin.Core.ViewModels
{
    public class CommissionGroupViewModel
    {
        public IEnumerable<Product> ProductList { get; set; }

        public IEnumerable<Deduction> DeductionList { get; set; }
    }
}
