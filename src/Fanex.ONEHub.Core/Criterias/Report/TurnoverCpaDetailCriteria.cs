namespace Fanex.Affiliate.Admin.Core.Criterias.Report
{
    using Fanex.Data;
    using Fanex.Data.Repository;

    public class TurnoverCpaDetailCriteria : CriteriaBase
    {
        [SpParam(SqlParamName = "MemberCPAID")]
        public int MemberCpaId { get; set; }

        public override string GetSettingKey() => "Aff_Admin_Report_PlayerCPA_TO_Detail";

        public override bool IsValid() => MemberCpaId > 0;
    }
}
