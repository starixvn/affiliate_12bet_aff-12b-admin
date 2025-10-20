using Fanex.Data;
using Fanex.Data.Repository;

namespace Fanex.Affiliate.Admin.Core.Criterias.Administrator
{
    public class AdministratorDetailCriteria : CriteriaBase
    {
        const string SP_NAME = "Aff_Con_AdminSelByCustID";

        public override string GetSettingKey() => SP_NAME;

        public override bool IsValid() => UserId > 0;

        [SpParam(SqlParamName = "CustId")]
        public int UserId { get; set; }
    }
}
