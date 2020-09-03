
namespace Fanex.Affiliate.Admin.Core.DTO.AdminRole
{
    public class UserPermissionInfo
    {
        public int FuncId { get; set; }

        public string FuncName { get; set; }

        public int UserPolicy { get; set; }

        public bool IsUserRead => UserPolicy == Constants.Policy.ReadButNotHost || UserPolicy == Constants.Policy.ReadAndHost;

        public bool IsUserHost => UserPolicy == Constants.Policy.ReadAndHost;

        public int RolePolicy { get; set; }

        public bool IsRoleRead => RolePolicy == Constants.Policy.ReadButNotHost || RolePolicy == Constants.Policy.ReadAndHost;

        public bool IsRoleHost => RolePolicy == Constants.Policy.ReadAndHost;
    }
}
