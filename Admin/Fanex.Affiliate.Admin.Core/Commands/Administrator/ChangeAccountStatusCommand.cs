namespace Fanex.Affiliate.Admin.Core.Commands.Administrator
{
    public class ChangeAccountStatusCommand : BaseNonQueryCommand
    {
        private const string SP_NAME = "Aff_Con_AdminUpdClosed";

        public override string GetSettingKey() => SP_NAME;

        public override bool IsValid() => true;

        public int CustId { get; set; }

        public bool Closed { get; set; }
    }
}
