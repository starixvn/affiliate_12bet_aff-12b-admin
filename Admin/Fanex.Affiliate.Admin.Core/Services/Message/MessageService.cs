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

            return ExecuteReturnResultInfo(command);
        }

        public ResultInfo UpdateMessage(InsertUpdateMessageParam param)
        {
            var command = new UpdateMessageCommand
            {
                MessageId = param.MessageId,
            };
            AssignCommand(command, param);

            return ExecuteReturnResultInfo(command);
        }

        public ResultInfo DeleteMessages(DeleteMessageParam param)
        {
            var command = new DeleteMessageCommand
            {
                UserId = param.UserId,
                Ip = param.Ip,
                MessageIds = param.MessageIdList,
            };

            return ExecuteReturnResultInfo(command);
        }

        private static void AssignCommand(BaseMessageCommand command, InsertUpdateMessageParam param)
        {
            command.UserId = param.UserId;
            command.Ip = param.Ip;
            command.IsImportant = param.Important;
            command.MsgBn = param.Message_bn;
            command.MsgEn = param.Message_en;
            command.MsgCn = param.Message_cs;
            command.MsgTh = param.Message_th;
            command.MsgVi = param.Message_vn;
            command.MsgId = param.Message_id;
            command.MsgKh = param.Message_kh;
            command.MsgHi = param.Message_hi;
            command.MsgMs = param.Message_ms;
        }
    }
}
