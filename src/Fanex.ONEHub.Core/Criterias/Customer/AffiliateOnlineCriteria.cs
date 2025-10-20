using Fanex.Data.Repository;

namespace Fanex.Affiliate.Admin.Core.Criterias.Customer
{
    public class AffiliateOnlineCriteria : CriteriaBase
    {
        private const string SP_NAME = "Aff_Admin_AffiliateOnlineList_Sel";

        public override string GetSettingKey() => SP_NAME;

        public override bool IsValid() => !string.IsNullOrWhiteSpace(Username);

        public string Username { get; set; }
    }
}
