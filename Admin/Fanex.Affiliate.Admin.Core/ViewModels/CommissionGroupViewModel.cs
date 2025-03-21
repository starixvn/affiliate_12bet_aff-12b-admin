using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.CommissionGroup;
using System.Collections.Generic;

namespace Fanex.Affiliate.Admin.Core.ViewModels
{
    public class CommissionGroupViewModel
    {
        public IEnumerable<Product> ProductList { get; set; }

        public IEnumerable<DeductionItem> DeductionList { get; set; }

        public IEnumerable<SubMenu> SubMenuList { get; set; }

        public IEnumerable<CommGroupType> CommGroupTypeList { get; set; }
    }
}
