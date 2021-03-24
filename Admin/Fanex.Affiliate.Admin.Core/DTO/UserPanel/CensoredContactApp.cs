using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;

namespace Fanex.Affiliate.Admin.Core.DTO.UserPanel
{
    public class CensoredContactApp: ContactApp
    {
        [CensoredPermissionCheck(Function.AffPersonalInfo)]
        public new string Value { get; set; }
    }
}
