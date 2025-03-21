using Fanex.Data;

namespace Fanex.Affiliate.Admin.Core.Commands.CommissionGroup
{
    public abstract class BaseCommGroupCommand : BaseNonQueryCommand
    {
        public int GroupId { get; set; }

        public string GroupName { get; set; }

        [SpParam(SqlParamName = "SubCommRate")]
        public decimal SubAffCommRate { get; set; }

        public decimal QmTurnover { get; set; }

        public bool IsActivePlayerSum { get; set; }

        public string Description { get; set; }

        [SpParam(SqlParamName = "DeductionList")]
        public string TierDeductionsXml { get; set; }

        [SpParam(SqlParamName = "TierList")]
        public string TierSettingXml { get; set; }
    }
}
