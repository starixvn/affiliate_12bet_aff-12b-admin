
using Newtonsoft.Json;

namespace Fanex.Affiliate.Admin.Core.DTO.AdminRole
{
    public class UserPermissionInfo
    {
        public int ParentId { get; set; }

        public string AllParentIds { get; set; }

        public int FuncId { get; set; }

        public string FuncName { get; set; }

        public string DisplayName { get; set; }

        [JsonIgnore]
        public int Policy { get; set; }

        public bool IsRead => Policy == Constants.Policy.ReadButNotHost || Policy == Constants.Policy.ReadAndHost;

        public bool IsHost => Policy == Constants.Policy.ReadAndHost;

        [JsonIgnore]
        public int RolePolicy { get; set; }

        public bool IsRoleRead => RolePolicy == Constants.Policy.ReadButNotHost || RolePolicy == Constants.Policy.ReadAndHost;

        public bool IsRoleHost => RolePolicy == Constants.Policy.ReadAndHost;
    }
}
