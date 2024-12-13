namespace Fanex.Affiliate.Admin.Core.Commands.CommissionGroup
{
    public class DeleteCommGroupCommand : BaseNonQueryCommand
    {
        private const string SP_NAME = "Aff_Admin_CommGroup_Del";

        public override string GetSettingKey() => SP_NAME;

        public override bool IsValid() => GroupId > 0;

        public int GroupId { get; set; }
    }
}
