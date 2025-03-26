using Fanex.Data.Repository;

namespace Fanex.Affiliate.Admin.Core.Criterias.CommissionGroup
{
    public class GetNormalCommGroupCriteria : CriteriaBase
    {
        private const string SP_NAME = "Aff_Admin_CommGroupNormal_Sel";

        public override string GetSettingKey() => SP_NAME;

        public override bool IsValid() => true;

        public int AffId { get; set; }

        public int GroupId { get; set; }
    }
}
