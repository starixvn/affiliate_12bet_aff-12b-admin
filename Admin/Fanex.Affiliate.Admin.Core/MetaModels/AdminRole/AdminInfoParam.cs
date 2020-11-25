using Fanex.Affiliate.Admin.Core.Constants;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Fanex.Affiliate.Admin.Core.MetaModels.AdminRole
{
    public class AdminInfoParam
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "User Name is required!")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "First Name is required!")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required!")]
        public string LastName { get; set; }

        public string Password { get; set; }

        [Range(Validation.MinId, Validation.MaxRoleId, ErrorMessage = "Role is invalid!")]
        public int? Role { get; set; }

        public bool? IsClosed { get; set; }

        public bool? IsRoleMarketDefault { get; set; }

        public bool? IsRoleFunctionDefault { get; set; }

        public int ByUserId { get; set; }

        public string Ip { get; set; }

        public IEnumerable<RolePermissionParam> PermissionInfo { get; set; }

        public string PermissionInfoXml { get; set; }

        public IEnumerable<UserMarketParam> MarketList { get; set; }

        public string MarketListXml { get; set; }
    }
}
