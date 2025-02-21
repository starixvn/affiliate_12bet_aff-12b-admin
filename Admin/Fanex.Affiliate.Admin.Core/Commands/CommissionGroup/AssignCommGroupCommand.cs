
namespace Fanex.Affiliate.Admin.Core.Commands.CommissionGroup
{
    public class AssignCommGroupCommand : BaseAffNonQueryCommand
    {
        private const string SP_NAME = "Aff_Admin_CommGroup_Assign";

        public override string GetSettingKey() => SP_NAME;

        public override bool IsValid() => GroupId > 0 && AffId > 0;

        public int GroupId { get; set; }
    }
}
