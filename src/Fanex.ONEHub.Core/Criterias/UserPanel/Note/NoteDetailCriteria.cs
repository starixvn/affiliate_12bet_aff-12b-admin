using Fanex.Data.Repository;

namespace Fanex.Affiliate.Admin.Core.Criterias.UserPanel
{
    public class NoteDetailCriteria : CriteriaBase
    {
        const string SP_NAME = "Aff_Admin_AffiliateNote_Detail_Sel";

        public override string GetSettingKey() => SP_NAME;

        public override bool IsValid() => AffId > 0 && NoteId > 0;

        public int AffId { get; set; }

        public int NoteId { get; set; }
    }
}
