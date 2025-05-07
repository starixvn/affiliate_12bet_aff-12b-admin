using Fanex.Data;
using System;

namespace Fanex.Affiliate.Admin.Core.Criterias.UserPanel
{
    public class PlayerDetailCriteria : AffPagingCriteria
    {
        const string SP_NAME = "Aff_Admin_PlayersDetail_Sel";

        public override string GetSettingKey() => SP_NAME;

        public override bool IsValid() => AffId > 0;

        public int ProductId { get; set; }

        public DateTime MonthDate { get; set; }

        public bool IsSortAsc { get; set; }

        public bool IsExportExcel { get; set; }

        public bool IsIncludeRemovedPlayer { get; set; }

        public int SortById { get; set; }

        [SpParam(SqlParamName = "BaseCurrency")]
        public int BaseCurrencyId { get; set; }

        [SpParam(SqlParamName = "Currencies")]
        public string CurrencyIds { get; set; }
    }
}
