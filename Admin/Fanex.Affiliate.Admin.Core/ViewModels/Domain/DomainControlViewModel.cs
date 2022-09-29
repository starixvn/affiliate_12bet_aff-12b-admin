using Fanex.Affiliate.Admin.Core.DTO.Common;
using System.Collections.Generic;

namespace Fanex.Affiliate.Admin.Core.ViewModels.Domain
{
    public class DomainControlViewModel
    {
        public IEnumerable<SubMenu> SubMenuList { get; set; }

        public IEnumerable<Language> MarketList { get; set; }
    }
}
