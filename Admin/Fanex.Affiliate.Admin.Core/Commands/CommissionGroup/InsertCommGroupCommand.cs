
namespace Fanex.Affiliate.Admin.Core.Commands.CommissionGroup
{
    public class InsertCommGroupCommand : BaseCommGroupCommand
    {
        private const string SP_NAME = "Aff_Admin_CommGroup_Ins";

        public override string GetSettingKey() => SP_NAME;

        public override bool IsValid() => true;

        public bool IsPrivate { get; set; }
    }
}
