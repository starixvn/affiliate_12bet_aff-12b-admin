using Fanex.Data.Repository;

namespace Fanex.Affiliate.Admin.Core.Criterias.EmailHistory
{
    public class EmailHistoryDetailCriteria : CriteriaBase
    {
        const string SP_NAME = "Aff_Admin_EmailHistory_Detail_Sel";

        public EmailHistoryDetailCriteria(int selectedEmailId)
        {
            MID = selectedEmailId;
        }

        public override string GetSettingKey() => SP_NAME;

        public override bool IsValid() => MID > 0;

        public int MID { get; }
    }
}
