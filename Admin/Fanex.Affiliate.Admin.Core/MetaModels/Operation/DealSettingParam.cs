namespace Fanex.Affiliate.Admin.Core.MetaModels.Operation
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Fanex.Affiliate.Admin.Core.Constants;
    using Fanex.Affiliate.Admin.Core.DTO.Operation;

    public class DealSettingParam : UserInfoParam
    {
        public int DealType { get; set; }

        public int GroupId { get; set; }

        public string DealName { get; set; }

        public decimal SubCommRate { get; set; }

        public string Description { get; set; }

        public string CountryCodes { get; set; }

        public short ActiveDays { get; set; }

        public bool IsActivePlayerHoldingPeriod { get; set; }

        public short HoldingDays { get; set; }

        public short HoldingTickets { get; set; }

        public bool IsQuota { get; set; }

        public short Quota { get; set; }

        public bool CheckedMinFdm { get; set; }

        public bool CheckedMinTotalDeposit { get; set; }

        public bool CheckedMinTurnover { get; set; }

        public bool IsInstant { get; set; }

        public IEnumerable<CpaTierSetting> CPATierSetting { get; set; }

        public IEnumerable<CpaGroupProduct> ProductGroup { get; set; }

        public decimal QmTurnover { get; set; }

        public bool IsActivePlayerSum { get; set; }

        public IEnumerable<RevenueTierSetting> RevenueTierSetting { get; set; }

        public IEnumerable<RevenueDeductionSetting> PlatformFeeSetting { get; set; }

        public RevenueDeductionSetting DepositFeeSetting { get; set; }

        public RevenueDeductionSetting WithdrawalFeeSetting { get; set; }
    }
}
