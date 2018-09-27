using System.Collections.Generic;
using Fanex.Affiliate.User.Core.DTO;
using Fanex.Affiliate.User.Core.DTO.UserPanel;
using Fanex.Affiliate.User.Core.MetalModels.UserPanel;

namespace Fanex.Affiliate.User.Core.Repository.UserPanel
{
    public interface INewsRepository
    {
        LatestHotNews GetLastestHotNews(bool isInternal);

        IEnumerable<MessageHistory> GetMessageList(MessageHistoryParam param);
    }
}
