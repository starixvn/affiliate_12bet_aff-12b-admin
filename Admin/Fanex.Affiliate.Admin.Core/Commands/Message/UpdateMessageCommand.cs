
namespace Fanex.Affiliate.Admin.Core.Commands.Message
{
    public class UpdateMessageCommand : BaseMessageCommand
    {
        private const string SP_NAME = "Aff_Admin_Message_Upd";

        public override string GetSettingKey() => SP_NAME;

        public override bool IsValid() => MessageId > 0;

        public int MessageId { get; set; }
    }
}
