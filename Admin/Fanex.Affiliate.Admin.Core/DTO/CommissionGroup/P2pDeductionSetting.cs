using Fanex.Affiliate.Admin.Core.Constants;

namespace Fanex.Affiliate.Admin.Core.DTO.CommissionGroup
{
    public class P2pDeductionSetting : DeductionSetting
    {
        public override int DeductionId => Deduction.P2pDeductionId;

        public override string DeductionName => Deduction.P2pDeductionName;
    }
}
