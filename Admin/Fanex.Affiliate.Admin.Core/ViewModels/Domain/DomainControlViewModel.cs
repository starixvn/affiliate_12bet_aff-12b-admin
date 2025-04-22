using Fanex.Affiliate.Admin.Core.DTO.Common;
using System.Collections.Generic;

namespace Fanex.Affiliate.Admin.Core.ViewModels.Domain
{
    public class DomainControlViewModel
    {
        public IEnumerable<TargetCountry> TargetCountryList { get; set; }

        public IEnumerable<string> AffSubDomainList { get; set; }
    }
}
