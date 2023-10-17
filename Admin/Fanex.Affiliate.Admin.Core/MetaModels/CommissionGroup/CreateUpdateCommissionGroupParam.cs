using Fanex.Affiliate.Admin.Core.DTO.CommissionGroup;
using System.Collections.Generic;

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

        public string FdmList { get; set; }

        public decimal QmTurnover { get; set; }

        public string Ip { get; set; }

        public bool IsApplyBonus { get; set; }

        public IEnumerable<TierSetting> TierSettingList { get; set; }

        public IEnumerable<FdmSetting> FdmSettingList { get; set; }

        public IEnumerable<DeductionSetting> PlatformFeeList { get; set; }

        public DeductionSetting OperationalFee { get; set; }

        public DeductionSetting BankingFee { get; set; }

        public DeductionSetting ExchangeWinLossShare { get; set; }

        public DeductionSetting ExchangeCommissionShare { get; set; }
    }
}
