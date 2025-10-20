using Fanex.Data.Repository;

namespace Fanex.Affiliate.Admin.Core.Criterias.Administrator
{
    public class PoliciesByGroupIdsCriteria : CriteriaBase
    {
        const string SP_NAME = "Aff_Con_Batch_AdminPolicySel";

        public override string GetSettingKey() => SP_NAME;

        public override bool IsValid() => true;

        public string CustIds { get; set; }

        public int GroupId { get; set; }
    }
}
