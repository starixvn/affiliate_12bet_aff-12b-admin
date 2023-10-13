using Fanex.Data;

namespace Fanex.Affiliate.Admin.Core.MetaModels.Domain
{
    public class TargetCountryParam
    {
        public int UserId { get; set; }

        public int AffDomainId { get; set; }

        [SpParam(SqlParamName = "12BetDomainId")]
        public int RedirectDomainId { get; set; }
    }
}
