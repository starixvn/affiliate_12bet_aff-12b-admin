namespace Fanex.Affiliate.Admin.Core.ViewModels.Operation
{
    using System.Collections.Generic;
    using DTO.Affiliate;
    using DTO.Operation;

    public class AssignDealViewModel
    {
        public IEnumerable<NormalDeal> DealList { get; set; }

        public IEnumerable<DealType> DealTypeList { get; set; }

        public AffiliateBasicInfo AffInfo { get; set; }
    }
}
