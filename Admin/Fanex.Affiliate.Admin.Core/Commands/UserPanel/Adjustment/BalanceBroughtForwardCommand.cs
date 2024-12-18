
namespace Fanex.Affiliate.Admin.Core.Commands.UserPanel.Adjustment
{
    public class BalanceBroughtForwardCommand : BaseAffNonQueryCommand
    {
        private const string SP_NAME = "Aff_CustomerSelBalBF";

        public override string GetSettingKey() => SP_NAME;

        public override bool IsValid() => AffId > 0;

        public decimal BalBroughtForward { get; set; }
    }
}
