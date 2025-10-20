namespace Fanex.Affiliate.Admin.Core.Commands.Administrator
{
    public class UpdateRolePolicyCommand : BaseNonQueryCommand
    {
        private const string SP_NAME = "Aff_Con_GroupPolicyUpd";

        public override string GetSettingKey() => SP_NAME;

        public override bool IsValid() => true;

        public string GroupPolicys { get; set; }
    }
}
