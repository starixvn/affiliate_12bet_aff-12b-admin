namespace Fanex.Affiliate.Admin.Core.Commands.Administrator
{
    public class InsertAccountCommand : BaseAccountCommand
    {
        private const string SP_NAME = "Aff_Con_AdminIns";

        public override string GetSettingKey() => SP_NAME;

        public override bool IsValid() => true;

        public string UserName { get; set; }

        public string UserPwd { get; set; }
    }
}
