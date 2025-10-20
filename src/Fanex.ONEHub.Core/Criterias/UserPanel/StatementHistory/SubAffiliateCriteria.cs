using Fanex.Data;
using System;

namespace Fanex.Affiliate.Admin.Core.Criterias.UserPanel.StatementHistory
{
    public class SubAffiliateCriteria : AffPagingCriteria
    {
        const string SP_NAME = "Aff_SubAffiliate_Commission_Sel";

        public override string GetSettingKey() => SP_NAME;

        public override bool IsValid() => AffId > 0;

        public DateTime MonthDate { get; set; }

        [SpParam(SqlParamName = "BaseCurrency")]
        public int CurrencyId { get; set; }
    }
}
