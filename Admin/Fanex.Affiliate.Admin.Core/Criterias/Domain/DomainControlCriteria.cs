using Fanex.Data.Repository;

namespace Fanex.Affiliate.Admin.Core.Criterias.Domain
{
    public class DomainControlCriteria : CriteriaBase
    {
        const string SP_NAME = "Aff_Admin_DomainControl_Sel";

        public override string GetSettingKey() => SP_NAME;

        public override bool IsValid() => true;

        public string MarketId { get; set; }
    }
}
