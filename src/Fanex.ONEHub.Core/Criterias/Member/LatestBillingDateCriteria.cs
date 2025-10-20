using Fanex.Data.Repository;

namespace Fanex.Affiliate.Admin.Core.Criterias.Member
{
    public class LatestBillingDateCriteria : CriteriaBase
    {
        const string SP_NAME = "Task_Aff_GetSetting";

        public override string GetSettingKey() => SP_NAME;

        public override bool IsValid() => true;

        public string SName { get; } = "AFF_CommissionDate";
    }
}
