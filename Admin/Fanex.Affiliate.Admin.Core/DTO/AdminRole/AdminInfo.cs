
namespace Fanex.Affiliate.Admin.Core.DTO.AdminRole
{
    public class AdminInfo
    {
        public int UserId { get; set; }

        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int RoleId { get; set; }

        public bool IsActive { get; set; }

        public bool IsRoleMarketDefault { get; set; }

        public bool IsRoleFunctionDefault { get; set; }
    }
}
