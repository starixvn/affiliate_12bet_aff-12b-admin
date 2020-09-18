
using Fanex.Affiliate.Admin.Core.Constants;
using System.ComponentModel.DataAnnotations;

namespace Fanex.Affiliate.Admin.Core.MetaModels.AdminRole
{
    public class MarketInfoParam: UserMarketParam
    {
        [Range(Validation.MinId, Validation.MaxId, ErrorMessage = "Role Id is invalid!")]
        public int RoleId { get; set; }
    }
}
