namespace Fanex.Affiliate.User.Core.Criterias
{
    using System;
    using Utilities;

    public class GetDailyEarningCriteria : BaseCriteria
    {
        private const string SpKey = "Aff_DailyEarning_Sel";

        public int AffId { get; set; }

        public byte? ProductId { get; set; }

        public int BaseCurrency { get; set; }

        public DateTime? MonthDate { get; set; }

        public override string GetSettingKey() => DbSettingHelper.BuildSpKey(IsInternal, SpKey);

        public override bool IsValid() => AffId > 0 && BaseCurrency > 0;
    }
}
