using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.EmailHistory;
using Fanex.Affiliate.Admin.Core.MetaModels.EmailHistory;

namespace Fanex.Affiliate.Admin.Core.Services.EmailHistory
{
    public interface IEmailHistoryService
    {
        EntityResult<EmailHistoryInfo> GetEmailHistoryList(EmailHistoryParam param);
    }
}
