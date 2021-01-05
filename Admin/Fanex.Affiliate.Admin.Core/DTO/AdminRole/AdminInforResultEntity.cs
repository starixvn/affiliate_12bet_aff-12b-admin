using System.Collections.Generic;

namespace Fanex.Affiliate.Admin.Core.DTO.AdminRole
{
    public class AdminInforResultEntity
    {
        public AdminInfo AdminInfo { get; set; }

        public IEnumerable<MarketInfo> MarketList { get; set; }

        public IEnumerable<UserPermissionInfo> UserPermissionList { get; set; }
    }
}
