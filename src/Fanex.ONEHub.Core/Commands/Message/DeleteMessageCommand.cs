
namespace Fanex.Affiliate.Admin.Core.Commands.Message
{
    public class DeleteMessageCommand : BaseNonQueryCommand
    {
        private const string SP_NAME = "Aff_Admin_Message_Del";

        public override string GetSettingKey() => SP_NAME;

        public override bool IsValid() => !string.IsNullOrWhiteSpace(MessageIds);

        public string MessageIds { get; set; }
    }
}
