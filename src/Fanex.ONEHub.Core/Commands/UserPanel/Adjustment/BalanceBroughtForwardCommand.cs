using Fanex.Data.Repository;

namespace Fanex.Affiliate.Admin.Core.Commands.UserPanel.Adjustment
{
    public class BalanceBroughtForwardCommand : NonQueryCommand
    {
        private const string SP_NAME = "Aff_CustomerSelBalBF";

        public override string GetSettingKey() => SP_NAME;

        public override bool IsValid() => AffId > 0;

        public int AffId { get; set; }

        public decimal BalBroughtForward { get; set; }
    }
}
