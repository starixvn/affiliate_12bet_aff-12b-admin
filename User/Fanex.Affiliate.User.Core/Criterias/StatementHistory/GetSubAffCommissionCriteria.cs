namespace Fanex.Affiliate.User.Core.Criterias
{
    using System;
    using Data;
    using Data.Repository;
    using Utilities;

    public class GetSubAffCommissionCriteria : CriteriaBase
    {
        private const string SpKey = "Aff_SubAffiliate_Commission_Sel";

        public int AffId { get; set; }

        public DateTime? MonthDate { get; set; }

        public int BaseCurrency { get; set; }

        [SpParam(Ignored = true)]
        public bool IsInternal { get; set; }

        public override string GetSettingKey() => DbSettingHelper.BuildSpKey(IsInternal, SpKey);

        public override bool IsValid() => AffId > 0 && BaseCurrency > 0;
    }
}
