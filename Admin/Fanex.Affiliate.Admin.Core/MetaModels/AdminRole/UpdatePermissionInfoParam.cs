using Fanex.Affiliate.Admin.Core.Constants;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Fanex.Affiliate.Admin.Core.MetaModels.AdminRole
{
    public class UpdatePermissionInfoParam : UserInfoParam
    {
        [Range(Validation.MinId, Validation.MaxId, ErrorMessage = "Role Id is invalid!")]
        public int RoleId { get; set; }

        public IEnumerable<PermissionInfoParam> PermissionInfo { get; set; }

        public string PermissionInfoXml { get; set; }
    }
}
