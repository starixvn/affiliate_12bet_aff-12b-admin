namespace Fanex.Affiliate.Admin.Core.Commands.Customer
{
    public class UpdateTransferLimitCommand : BaseAffNonQueryCommand
    {
        private const string SP_NAME = "Aff_Admin_TransferLimit_Upd";

        public override string GetSettingKey() => SP_NAME;

        public override bool IsValid() => AffId > 0;

        public bool DisabledTransfer { get; set; }

        public decimal TransferLimit { get; set; }
    }
}
