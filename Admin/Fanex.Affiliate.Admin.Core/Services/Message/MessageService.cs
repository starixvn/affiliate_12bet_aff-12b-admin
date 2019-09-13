using System;
using System.Collections.Generic;
using Fanex.Affiliate.Admin.Core.DTO.Message;
using Fanex.Affiliate.Admin.Core.MetaModels.Message;
using Fanex.Affiliate.Admin.Core.Repository.Message;
using Fanex.Affiliate.Admin.Core.DTO;

namespace Fanex.Affiliate.Admin.Core.Services.Message
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _messageRepository;

        public MessageService(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }

        public EntityResult<MessageInfo> GetMessageList(MessageParam param)
        {
            if (param.IsGetAll)
            {
                param.ToDate = null;
                param.FromDate = null;
            }
            else
            {
                param.ToDate = param.ToDate.Value.AddDays(1).AddMilliseconds(-3);
            }
            return _messageRepository.GetMessageList(param);
        }

        public MessageDetail GetMessageDetail(int selectedMessageId)
        {
            return _messageRepository.GetMessageDetail(selectedMessageId);
        }

        public ResultInfo InsertMessage(InsertUpdateMessageParam param)
        {
            return _messageRepository.InsertMessage(param);
        }

        public ResultInfo UpdateMessage(InsertUpdateMessageParam param)
        {
            return _messageRepository.UpdateMessage(param);
        }

        public ResultInfo DeleteMessage(DeleteMessageParam param)
        {
            return _messageRepository.DeleteMessage(param);
        }
    }
}
