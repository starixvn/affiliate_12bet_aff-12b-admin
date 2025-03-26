using Fanex.Data.Repository;

namespace Fanex.Affiliate.Admin.Core.Criterias.CommissionGroup
{
    public class GetCommissionGroupCriteria : CriteriaBase
    {
        private const string SP_NAME = "Aff_Admin_CommGroup_Sel";

        public override string GetSettingKey() => SP_NAME;

        public override bool IsValid() => true;

        public int GroupId { get; set; }

        public string GroupName { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public int Nrows { get; set; }
    }
}
