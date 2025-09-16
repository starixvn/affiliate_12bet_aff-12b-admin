using Fanex.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fanex.Affiliate.Admin.Core.Commands.LandingPage
{
    public abstract class BasePageBuilderCommand : BaseNonQueryCommand
    {
        public string PageName { get; set; }
        public int CampaignId { get; set; }
        public string ComponentStructure { get; set; }
        public string TargetCountryCode { get; set; }
        [SpParam(SqlParamName = "MarketId")]
        public string LanguageCode { get; set; }
        public string DealIds { get; set; }
        public int StatusId { get; set; }
        public int PageId { get; set; }
    }
}
