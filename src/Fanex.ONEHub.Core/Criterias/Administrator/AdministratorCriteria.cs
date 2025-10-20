using Fanex.Data.Repository;

namespace Fanex.Affiliate.Admin.Core.Criterias.Administrator
{
    public class AdministratorCriteria : CriteriaBase
    {
        const string SP_NAME = "Aff_Con_AdminSelByGroupID";

        public override string GetSettingKey() => SP_NAME;

        public override bool IsValid() => true;

        public int? GroupId { get; set; }

        public string UserName { get; set; }

        public string FirstChar { get; set; }
    }
}
