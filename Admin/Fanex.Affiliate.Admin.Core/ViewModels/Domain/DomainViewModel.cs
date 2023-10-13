using Fanex.Affiliate.Admin.Core.DTO.Domain;
using System.Collections.Generic;

namespace Fanex.Affiliate.Admin.Core.ViewModels.Domain
{
    public class DomainViewModel
    {
        public IEnumerable<TargetCountry> TargetCountryList { get; set; }

        public bool IsAffDomain { get; set; }
    }
}
