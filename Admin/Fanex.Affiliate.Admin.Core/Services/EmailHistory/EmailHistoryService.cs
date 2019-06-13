using Fanex.Affiliate.Admin.Core.DTO.EmailHistory;
using Fanex.Affiliate.Admin.Core.MetaModels.EmailHistory;
using Fanex.Affiliate.Admin.Core.Repository;
using Fanex.Affiliate.Admin.Core.DTO;

namespace Fanex.Affiliate.Admin.Core.Services.EmailHistory
{
    public class EmailHistoryService : IEmailHistoryService
    {
        private readonly IEmailHistoryRepository _emailHistoryRepository;

        public EmailHistoryService(IEmailHistoryRepository emailHistoryRepository)
        {
            _emailHistoryRepository = emailHistoryRepository;
        }

        public ResultInfo DeleteEmailHistory(DeleteEmailHistoryParam param)
        {
            return _emailHistoryRepository.DeleteEmailHistory(param);
        }

        public EmailHistoryInfo GetEmailHistoryDetail(int selectedEmailId)
        {
            return _emailHistoryRepository.GetEmailHistoryDetail(selectedEmailId);
        }

        public EntityResult<EmailHistoryInfo> GetEmailHistoryList(EmailHistoryParam param)
        {
            param.ToDate = param.ToDate.AddDays(1).AddMilliseconds(-3);

            return _emailHistoryRepository.GetEmailHistoryList(param);
        }
    }
}
