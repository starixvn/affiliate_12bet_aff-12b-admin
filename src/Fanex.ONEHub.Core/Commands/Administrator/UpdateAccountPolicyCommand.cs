namespace Fanex.Affiliate.Admin.Core.Commands.Administrator
{
    public class UpdateAccountPolicyCommand : BaseNonQueryCommand
    {
        private const string SP_NAME = "Aff_Con_AdminPolicyUpd";

        public override string GetSettingKey() => SP_NAME;

        public override bool IsValid() => true;

        public string MemPolicys { get; set; }
    }
}
