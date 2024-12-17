using Fanex.Affiliate.Admin.Core.Commands.Message;
using Fanex.Affiliate.Admin.Core.Criterias.Message;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Message;
using Fanex.Affiliate.Admin.Core.MetaModels.Message;
using Fanex.Data.Repository;

namespace Fanex.Affiliate.Admin.Core.Services.Message
{
    public class MessageService : BaseDynamicService, IMessageService
    {
        public MessageService(
            IDynamicRepository dynamicRepository) : base(dynamicRepository) { }

        public EntityResult<MessageInfo> GetMessageList(MessageParam param)
        {
            var criteria = new MessageCriteria
            {
                SearchById = param.SearchById,
                IsGetAll = param.IsGetAll,
                FromDate = param.FromDate,
                ToDate = param.ToDate,
                PageIndex = param.PageIndex,
                PageSize = param.PageSize,
            };

            return FetchEntityResult<MessageInfo>(criteria);
        }

        public MessageDetail GetMessageDetail(int selectedMessageId)
        {
            var criteria = new MessageDetailCriteria
            {
                MessageId = selectedMessageId
            };

            return GetTData<MessageDetail>(criteria);
        }

        public ResultInfo InsertMessage(InsertUpdateMessageParam param)
        {
            var command = new InsertMessageCommand();
            AssignCommand(command, param);

            return ExecuteReturnResultInfo(command, param);
        }

        public ResultInfo UpdateMessage(InsertUpdateMessageParam param)
        {
            var command = new UpdateMessageCommand
            {
                MessageId = param.MessageId,
            };
            AssignCommand(command, param);

            return ExecuteReturnResultInfo(command, param);
        }

        public ResultInfo DeleteMessages(DeleteMessageParam param)
        {
            var command = new DeleteMessageCommand
            {
                MessageIds = param.MessageIdList,
            };

            return ExecuteReturnResultInfo(command, param);
        }

        private static void AssignCommand(BaseMessageCommand command, InsertUpdateMessageParam param)
        {
            command.IsImportant = param.Important;
            command.MsgBn = param.MessageContentbn;
            command.MsgEn = param.MessageContenten;
            command.MsgCn = param.MessageContentcs;
            command.MsgTh = param.MessageContentth;
            command.MsgVi = param.MessageContentvn;
            command.MsgId = param.MessageContentid;
            command.MsgKh = param.MessageContentkh;
            command.MsgHi = param.MessageContenthi;
            command.MsgMs = param.MessageContentms;
        }
    }
}
