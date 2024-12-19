namespace Fanex.Affiliate.Admin.Core.Commands.Administrator
{
    public class UpdateDepartmentCommand : BaseNonQueryCommand
    {
        private const string SP_NAME = "Aff_Admin_Affiliate_Password_Upd";

        public override string GetSettingKey() => SP_NAME;

        public override bool IsValid() => true;

        public string Password { get; set; }
    }
}
