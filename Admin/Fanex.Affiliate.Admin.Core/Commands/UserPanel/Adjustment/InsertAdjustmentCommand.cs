using Fanex.Data;

namespace Fanex.Affiliate.Admin.Core.Commands.UserPanel.Adjustment
{
    public class InsertAdjustmentCommand : BaseAffNonQueryCommand
    {
        private const string SP_NAME = "Aff_Admin_AffiliateAdjustment_Ins";

        public override string GetSettingKey() => SP_NAME;

        public override bool IsValid() => AffId > 0;

        public decimal Amount { get; set; }

        [SpParam(SqlParamName = "transdesc")]
        public string Description { get; set; }
    }
}
