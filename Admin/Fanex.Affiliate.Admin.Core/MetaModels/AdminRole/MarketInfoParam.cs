
using Fanex.Affiliate.Admin.Core.Constants;
using System.ComponentModel.DataAnnotations;

namespace Fanex.Affiliate.Admin.Core.MetaModels.AdminRole
{
    public class MarketInfoParam
    {
        [Range(Validation.MinId, Validation.MaxId, ErrorMessage = "Role Id is invalid!")]
        public int RoleId { get; set; }

        [Required(ErrorMessage = "Role Id is invalid!")]
        public string MarketId { get; set; }

        public bool HasPermission { get; set; }
    }
}
