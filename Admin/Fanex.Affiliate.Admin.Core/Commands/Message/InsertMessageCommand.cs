
namespace Fanex.Affiliate.Admin.Core.Commands.Message
{
    public class InsertMessageCommand : BaseMessageCommand
    {
        private const string SP_NAME = "Aff_Admin_Message_Ins";

        public override string GetSettingKey() => SP_NAME;

        public override bool IsValid() => true;
    }
}
