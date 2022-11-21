
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Data;
using System.ComponentModel.DataAnnotations;

namespace Fanex.Affiliate.Admin.Core.MetaModels.Domain
{
    public class InsertDomainControlParam : UserInfoParam
    {
        [Required]
        public string MarketId { get; set; }

        [Range(Validation.MinId, Validation.MaxId)]
        public int AffDomainId { get; set; }

        [Range(Validation.MinId, Validation.MaxId)]
        [SpParam(SqlParamName = "12BetDomainID")]
        public int RedirectDomainId { get; set; }

        public bool Status { get; set; }
    }
}
