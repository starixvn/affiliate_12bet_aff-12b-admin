using System;

namespace Fanex.Affiliate.Admin.Core.Criterias.UserPanel
{
    public class ViewLogCriteria : AffPagingCriteria
    {
        const string SP_NAME = "Aff_Admin_AffiliateLog_Sel";

        public override string GetSettingKey() => SP_NAME;

        public override bool IsValid() => AffId > 0;

        public int LogType { get; set; }

        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }
    }
}
