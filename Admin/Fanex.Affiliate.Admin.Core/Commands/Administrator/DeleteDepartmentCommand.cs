namespace Fanex.Affiliate.Admin.Core.Commands.Administrator
{
    public class DeleteDepartmentCommand : BaseNonQueryCommand
    {
        private const string SP_NAME = "Aff_Con_GroupInfoDel";

        public override string GetSettingKey() => SP_NAME;

        public override bool IsValid() => GroupId > 0;

        public int GroupId { get; set; }
    }
}
