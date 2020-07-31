
namespace Fanex.Affiliate.Admin.Core.DTO.AdminRole
{
    public class AdminInfo
    {
        public int UserId { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public int RoleId { get; set; }

        public bool IsActive { get; set; }
    }
}
