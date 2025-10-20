
namespace Fanex.Affiliate.Admin.Core.Commands.Affiliate
{
    public class ChangeAffPasswordCommand : BaseAffNonQueryCommand
    {
        private const string SP_NAME = "Aff_Admin_Affiliate_Password_Upd";

        public override string GetSettingKey() => SP_NAME;

        public override bool IsValid() => AffId > 0;

        public string Password { get; set; }
    }
}
