
namespace Fanex.Affiliate.Admin.Core.Commands.Customer
{
    public class UpdateStatusCommand : BaseAffNonQueryCommand
    {
        private const string SP_NAME = "Aff_Admin_AffiliateStatus_Upd";

        public override string GetSettingKey() => SP_NAME;

        public override bool IsValid() => AffId > 0;

        public string Status { get; set; }
    }
}
