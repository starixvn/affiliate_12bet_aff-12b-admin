using Fanex.Affiliate.Admin.Core.Constants;
using System.ComponentModel.DataAnnotations;

namespace Fanex.Affiliate.Admin.Core.MetaModels.AdminRole
{
    public class AdminInfoParam
    {
        [Range(Validation.MinId, Validation.MaxId, ErrorMessage = "User Id is invalid!")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "User Name is required!")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required!")]
        [RegularExpression("^(?=.*[0-9]+.*[0-9]+)(?=.*[a-zA-Z].*[a-zA-Z]).{6,}$", ErrorMessage = "Password is invalid!")]
        public string Password { get; set; }

        [Compare(nameof(Password), ErrorMessage = "Confirm password is incorrect!")]
        public string ConfirmPassword { get; set; }

        [Range(Validation.MinId, Validation.MaxRoleId, ErrorMessage = "Role is invalid!")]
        public int Role { get; set; }

        public string MarketIds { get; set; }

        public bool IsActive { get; set; }

        public int ByUserId { get; set; }

        public string Ip { get; set; }
    }
}
