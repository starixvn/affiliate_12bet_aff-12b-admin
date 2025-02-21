namespace Fanex.Affiliate.Admin.Core.Commands.Administrator
{
    public class ChangeAccountPasswordCommand : BaseNonQueryCommand
    {
        private const string SP_NAME = "Aff_Admin_ChangePassword";

        public override string GetSettingKey() => SP_NAME;

        public override bool IsValid() => CustId > 0;

        public int CustId { get; set; }

        public string NewPwd { get; set; }
    }
}
