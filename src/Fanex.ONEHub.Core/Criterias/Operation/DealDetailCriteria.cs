using Fanex.Data.Repository;

namespace Fanex.Affiliate.Admin.Core.Criterias.Operation
{
    public class DealDetailCriteria : CriteriaBase
    {
        public int GroupId { get; set; }

        public override string GetSettingKey() => "Aff_Admin_Deal_Detail_Sel";

        public override bool IsValid() => true;
    }
}
