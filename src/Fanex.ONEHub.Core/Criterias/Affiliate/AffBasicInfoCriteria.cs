using Fanex.Data;
using Fanex.Data.Repository;

namespace Fanex.Affiliate.Admin.Core.Criterias.Affiliate
{
    public class AffBasicInfoCriteria : CriteriaBase
    {
        private const string SP_NAME = "Aff_Admin_AffiliateInfo_Sel";

        public override string GetSettingKey() => SP_NAME;

        public override bool IsValid() => true;

        public int? AffId { get; set; }

        [SpParam(SqlParamName = "Username")]
        public string AffName { get; set; }
    }
}
