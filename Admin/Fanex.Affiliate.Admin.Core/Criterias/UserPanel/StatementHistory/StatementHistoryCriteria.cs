using Fanex.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fanex.Affiliate.Admin.Core.Criterias.UserPanel.StatementHistory
{
    public class StatementHistoryCriteria : AffPagingCriteria
    {
        const string SP_NAME = "Aff_Admin_StatementHistory_Sel";

        public override string GetSettingKey() => SP_NAME;

        public override bool IsValid() => AffId > 0;

        [SpParam(SqlParamName = "FDate")]
        public DateTime? FromDate { get; set; }

        [SpParam(SqlParamName = "TDate")]
        public DateTime? ToDate { get; set; }
    }
}
