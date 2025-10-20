using Fanex.Data.Repository;

namespace Fanex.Affiliate.Admin.Core.Criterias.Report
{
    public class ReportCutOffDateCriteria : CriteriaBase
    {
        const string SP_NAME = "Task_Aff_GetSetting";

        public override string GetSettingKey() => SP_NAME;

        public override bool IsValid() => true;

        public string SName { get; set; } = "Report_CutOffDate";
    }
}
