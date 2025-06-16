namespace Fanex.Affiliate.Admin.Core.DTO.Operation
{
    using System.Collections.Generic;

    public class DealSettingDetail : DealSettingInfo
    {
        public IEnumerable<RevevueTierSetting> RevevueTierSetting { get; set; }

        public IEnumerable<RevenueDeductionSetting> PlatformFeeSetting { get; set; }

        public RevenueDeductionSetting DepositFeeSetting { get; set; }

        public RevenueDeductionSetting WithdrawalFeeSetting { get; set; }

        public IEnumerable<CpaTierSetting> CPATierSetting { get; set; }

        public IEnumerable<CpaGroupProduct> ProductGroup { get; set; }
    }
}
