namespace Fanex.Affiliate.Admin.Core.Commands.Message
{
    public abstract class BaseMessageCommand : BaseNonQueryCommand
    {
        public bool IsImportant { get; set; }

        public string MsgEn { get; set; }

        public string MsgCn { get; set; }

        public string MsgTh { get; set; }

        public string MsgVi { get; set; }

        public string MsgId { get; set; }

        public string MsgKr { get; set; }
    }
}
