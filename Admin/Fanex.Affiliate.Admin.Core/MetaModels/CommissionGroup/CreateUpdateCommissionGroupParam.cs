using System.Collections.Generic;
using Fanex.Affiliate.Admin.Core.DTO.CommissionGroup;

namespace Fanex.Affiliate.Admin.Core.MetaModels.CommissionGroup
{
    public class CreateUpdateCommissionGroupParam
    {
        public int UserId { get; set; }

        public int GroupId { get; set; }

        public string GroupName { get; set; }

        public string Description { get; set; }

        public string DeductionList { get; set; }

        public string TierList { get; set; }

        public decimal QmTurnover { get; set; }

        public string Ip { get; set; }

        public IEnumerable<TierSetting> TierSettingList { get; set; }

        public IEnumerable<DeductionSetting> PlatformFeeList { get; set; }

        public DeductionSetting OperationalFee { get; set; }

        public DeductionSetting BankingFee { get; set; }

        public DeductionSetting ExchangeWinLossShare { get; set; }

        public DeductionSetting ExchangeCommissionShare { get; set; }
    }
}
