using System.Collections.Generic;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.CommissionGroup;

namespace Fanex.Affiliate.Admin.Core.ViewModels
{
    public class CommissionGroupViewModel
    {
        public IEnumerable<Product> ProductList { get; set; }

        public IEnumerable<Deduction> DeductionList { get; set; }

        public IEnumerable<SubMenu> SubMenuList { get; set; }
    }
}
