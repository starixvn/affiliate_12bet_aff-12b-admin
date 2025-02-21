using Fanex.Data.Repository;

namespace Fanex.Affiliate.Admin.Core.Criterias.Administrator
{
    public class DepartmentDetailCriteria : CriteriaBase
    {
        const string SP_NAME = "Aff_Con_GroupInfoSel";

        public override string GetSettingKey() => SP_NAME;

        public override bool IsValid() => GroupId > 0;

        public int GroupId { get; set; }
    }
}
