using System.Collections.Generic;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.MetaModels.UserPanel;

namespace Fanex.Affiliate.Admin.Core.ViewModels.UserPanel
{
    public class PlayerDetailViewModel
    {
        public IEnumerable<Product> ProductList { get; set; }

        public PlayerDetailParam Param { get; set; }
    }
}
