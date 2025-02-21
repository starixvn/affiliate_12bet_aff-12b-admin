using Fanex.Data.Repository;

namespace Fanex.Affiliate.Admin.Core.Criterias.Administrator
{
    public class AccountPolicyCriteria : CriteriaBase
    {
        const string SP_NAME = "Aff_Con_AdminPolicySelByCustID";

        public override string GetSettingKey() => SP_NAME;

        public override bool IsValid() => true;

        public int CustId { get; set; }

        public int GroupId { get; set; }
    }
}
