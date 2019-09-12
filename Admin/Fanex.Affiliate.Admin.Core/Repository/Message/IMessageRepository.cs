using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Message;
using Fanex.Affiliate.Admin.Core.MetaModels.Message;
using System.Collections.Generic;

namespace Fanex.Affiliate.Admin.Core.Repository.Message
{
    public interface IMessageRepository
    {
        EntityResult<MessageInfo> GetMessageList(MessageParam param);

        MessageDetail GetMessageDetail(int selectedMessageId);
    }
}
