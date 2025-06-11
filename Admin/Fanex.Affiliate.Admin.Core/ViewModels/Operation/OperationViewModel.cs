namespace Fanex.Affiliate.Admin.Core.ViewModels.Operation
{
    using System.Collections.Generic;
    using Fanex.Affiliate.Admin.Core.DTO;
    using Fanex.Affiliate.Admin.Core.DTO.Common;
    using Fanex.Affiliate.Admin.Core.DTO.Operation;

    public class OperationViewModel
    {
        public IEnumerable<DealType> DealTypes { get; set; }

        public IEnumerable<TargetCountry> TargetCountries { get; set; }

         public IEnumerable<Product> Products { get; set; }
    }
}
