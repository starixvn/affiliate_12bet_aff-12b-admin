
namespace Fanex.Affiliate.Admin.Core.DTO.AdminRole
{
    public class PermissionRoleInfo
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Policy { get; set; }

        public bool IsView => Policy == Constants.Policy.ViewButNotEdit || Policy == Constants.Policy.ViewAndEdit;

        public bool IsEdit => Policy == Constants.Policy.ViewAndEdit;
    }
}
