using Fanex.Data;

namespace Fanex.Affiliate.Admin.Core.Commands.Message
{
    public abstract class BaseMessageCommand : BaseNonQueryCommand
    {
        public bool IsImportant { get; set; }

        public string MsgBn { get; set; }

        public string MsgEn { get; set; }

        public string MsgCn { get; set; }

        public string MsgTh { get; set; }

        public string MsgVi { get; set; }

        public string MsgId { get; set; }

        public string MsgKh { get; set; }

        public string MsgHi { get; set; }

        public string MsgMs { get; set; }
    }
}
