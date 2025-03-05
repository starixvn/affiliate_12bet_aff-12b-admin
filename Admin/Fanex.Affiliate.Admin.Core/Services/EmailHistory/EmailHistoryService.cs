using Fanex.Affiliate.Admin.Core.Commands.Affiliate;
using Fanex.Affiliate.Admin.Core.Criterias.EmailHistory;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.EmailHistory;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.MetaModels.EmailHistory;
using Fanex.Data.Repository;

namespace Fanex.Affiliate.Admin.Core.Services.EmailHistory
{
    public class EmailHistoryService : BaseDynamicService, IEmailHistoryService
    {
        public EmailHistoryService(
            IDynamicRepository dynamicRepository) : base(dynamicRepository) { }

        public ResultInfo DeleteEmailHistory(DeleteEmailHistoryParam param)
        {
            var command = param.MapTo<DeleteEmailHistoryParam, DeleteEmailHistoryCommand>();

            return ExecuteReturnResultInfo(command);
        }

        public EmailHistoryDetailInfo GetEmailHistoryDetail(int selectedEmailId)
        {
            var criteria = new EmailHistoryDetailCriteria(selectedEmailId);

            return GetTData<EmailHistoryDetailInfo>(criteria);
        }

        public EntityResult<EmailHistoryInfo> GetEmailHistoryList(EmailHistoryParam param)
        {
            var criteria = param.MapTo<EmailHistoryParam, EmailHistoryCriteria>();

            return FetchEntityResult<EmailHistoryInfo>(criteria);
        }
    }
}
