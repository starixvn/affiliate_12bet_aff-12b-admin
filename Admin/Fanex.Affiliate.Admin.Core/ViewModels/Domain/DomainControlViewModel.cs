using System.Collections.Generic;
using Fanex.Affiliate.Admin.Core.DTO.CommissionGroup;
using Fanex.Affiliate.Admin.Core.DTO.Common;

namespace Fanex.Affiliate.Admin.Core.ViewModels.Domain
{
    public class DomainControlViewModel
    {
        public IEnumerable<TargetCountry> TargetCountryList { get; set; }

        public IEnumerable<string> AffSubDomainList { get; set; }

        public IEnumerable<NormalCommGroup> DealList { get; set; }
    }
}
