
namespace Fanex.Affiliate.Admin.Core.Commands.CommissionGroup
{
    public class UpdateCommGroupCommand : BaseCommGroupCommand
    {
        private const string SP_NAME = "Aff_Admin_CommGroup_Upd";

        public override string GetSettingKey() => SP_NAME;

        public override bool IsValid() => GroupId > 0;
    }
}
