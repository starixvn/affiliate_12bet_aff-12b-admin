using System.Collections.Generic;
using Fanex.Affiliate.User.Core.DTO.UserPanel;

namespace Fanex.Affiliate.User.Core.ViewModels.UserPanel
{
    public class MessageHistoryViewModel
    {
        public IEnumerable<MessageHistory> MessageList { get; set; }

        public int TotalItems { get; set; }
    }
}
