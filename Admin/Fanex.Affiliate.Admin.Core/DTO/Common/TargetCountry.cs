
using Fanex.Data;

namespace Fanex.Affiliate.Admin.Core.DTO.Common
{
    public class TargetCountry
    {
        [SpParam(SqlParamName = "MarketId")]
        public string TargetCountryId { get; set; }

        [SpParam(SqlParamName = "MarketName")]
        public string TargetCountryName { get; set; }

        public byte Status { get; set; }
    }
}
