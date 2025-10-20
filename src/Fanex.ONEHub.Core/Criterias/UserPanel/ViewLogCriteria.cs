namespace Fanex.Affiliate.Admin.Core.Criterias.UserPanel
{
    public class ViewLogCriteria : AffPagingByDateCriteria
    {
        const string SP_NAME = "Aff_Admin_AffiliateLog_Sel";

        public override string GetSettingKey() => SP_NAME;

        public override bool IsValid() => AffId > 0;

        public int LogType { get; set; }
    }
}
