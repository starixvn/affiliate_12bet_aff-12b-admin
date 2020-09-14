
namespace Fanex.Affiliate.Admin.Core.DTO.AdminRole
{
    public class RolePermissionInfo
    {
        public int RoleId { get; set; }

        public string RoleName { get; set; }

        public int ParentId { get; set; }

        public int FuncId { get; set; }

        public string FuncName { get; set; }

        public int Policy { get; set; }

        public bool IsRead => Policy == Constants.Policy.ReadButNotHost || Policy == Constants.Policy.ReadAndHost;

        public bool IsHost => Policy == Constants.Policy.ReadAndHost;
    }
}
