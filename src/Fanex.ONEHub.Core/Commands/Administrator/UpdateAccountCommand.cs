namespace Fanex.Affiliate.Admin.Core.Commands.Administrator
{
    public class UpdateAccountCommand : BaseAccountCommand
    {
        private const string SP_NAME = "Aff_Con_AdminUpd";

        public override string GetSettingKey() => SP_NAME;

        public override bool IsValid() => CustId > 0;
    }
}
