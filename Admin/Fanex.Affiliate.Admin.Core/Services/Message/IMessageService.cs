using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Message;
using Fanex.Affiliate.Admin.Core.MetaModels.Message;
using System.Collections.Generic;

namespace Fanex.Affiliate.Admin.Core.Services.Message
{
    public interface IMessageService
    {
        EntityResult<MessageInfo> GetMessageList(MessageParam param);
    }
}
