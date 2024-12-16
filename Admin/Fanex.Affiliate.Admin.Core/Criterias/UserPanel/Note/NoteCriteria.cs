
namespace Fanex.Affiliate.Admin.Core.Criterias.UserPanel.Note
{
    public class NoteCriteria : AffPagingCriteria
    {
        const string SP_NAME = "Aff_Admin_AffiliateNote_Sel";

        public override string GetSettingKey() => SP_NAME;

        public override bool IsValid() => AffId > 0;
    }
}
