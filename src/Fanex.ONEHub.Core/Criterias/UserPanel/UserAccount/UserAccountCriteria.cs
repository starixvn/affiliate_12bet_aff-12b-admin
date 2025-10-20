using Fanex.Data.Repository;

namespace Fanex.Affiliate.Admin.Core.Criterias.UserPanel.UserAccount
{
    public class UserAccountCriteria : CriteriaBase
    {
        const string SP_NAME = "Aff_Admin_AffiliateInfo_Sel";

        public override string GetSettingKey() => SP_NAME;

        public override bool IsValid() => AffId > 0;

        public int AffId { get; set; }
    }
}
