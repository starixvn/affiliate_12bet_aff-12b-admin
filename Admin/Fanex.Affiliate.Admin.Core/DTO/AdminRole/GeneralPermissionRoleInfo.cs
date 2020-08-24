using System.Collections.Generic;
using System.Linq;

namespace Fanex.Affiliate.Admin.Core.DTO.AdminRole
{
    public class GeneralPermissionRoleInfo
    {
        public int RoleId { get; set; }

        public string RoleName { get; set; }

        public bool IsRead => Children.All(c => c.IsRead);

        public bool IsHost => Children.All(c => c.IsHost);

        public IEnumerable<GeneralFunctionInfo> Children { get; set; }
    }
}
