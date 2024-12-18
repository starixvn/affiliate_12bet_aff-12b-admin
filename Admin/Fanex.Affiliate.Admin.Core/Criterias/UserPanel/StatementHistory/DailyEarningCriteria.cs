using Fanex.Data;
using Fanex.Data.Repository;
using System;

namespace Fanex.Affiliate.Admin.Core.Criterias.UserPanel.StatementHistory
{
    public class DailyEarningCriteria : CriteriaBase
    {
        private readonly string SpKey;

        public DailyEarningCriteria(string spKey)
        {
            SpKey = spKey;
        }

        public override string GetSettingKey() => SpKey;

        public override bool IsValid() => !string.IsNullOrWhiteSpace(SpKey) && AffId > 0;

        public int AffId { get; set; }

        public int ProductId { get; set; }

        public DateTime? MonthDate { get; set; }

        [SpParam(SqlParamName = "BaseCurrency")]
        public int BaseCurrencyId { get; set; }

        [SpParam(SqlParamName = "Currencies")]
        public string CurrencyIds { get; set; }
    }
}
