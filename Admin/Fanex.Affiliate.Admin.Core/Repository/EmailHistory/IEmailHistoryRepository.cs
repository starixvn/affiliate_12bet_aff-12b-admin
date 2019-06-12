using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.EmailHistory;
using Fanex.Affiliate.Admin.Core.MetaModels.EmailHistory;

namespace Fanex.Affiliate.Admin.Core.Repository
{
    public interface IEmailHistoryRepository
    {
        EntityResult<EmailHistoryInfo> GetEmailHistoryList(EmailHistoryParam param);

        EmailHistoryInfo GetEmailHistoryDetail(int selectedEmailId);

        ResultInfo DeleteEmailHistory(DeleteEmailHistoryParam param);
    }
}
