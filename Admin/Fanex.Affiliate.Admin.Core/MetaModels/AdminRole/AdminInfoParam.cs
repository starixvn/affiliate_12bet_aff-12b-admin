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

        public string Password { get; set; }

        [Range(Validation.MinId, Validation.MaxRoleId, ErrorMessage = "Role is invalid!")]
        public int? Role { get; set; }

        public string MarketIds { get; set; }

        public bool? IsClosed { get; set; }

        public int ByUserId { get; set; }

        public string Ip { get; set; }

        public IEnumerable<RolePermissionParam> PermissionInfo { get; set; }

        public string PermissionInfoXml { get; set; }

        public IEnumerable<UserMarketParam> MarketList { get; set; }

        public string MarketListXml { get; set; }
    }
}
