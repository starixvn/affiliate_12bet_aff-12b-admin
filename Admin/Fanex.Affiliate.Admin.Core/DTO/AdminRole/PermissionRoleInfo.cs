
namespace Fanex.Affiliate.Admin.Core.DTO.AdminRole
{
    public class PermissionRoleInfo
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Policy { get; set; }

        public bool IsView { get; set; }

        public bool IsEdit { get; set; }
    }
}
