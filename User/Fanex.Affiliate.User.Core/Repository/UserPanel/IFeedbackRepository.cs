using Fanex.Affiliate.User.Core.DTO;
using Fanex.Affiliate.User.Core.DTO.UserPanel;
using Fanex.Affiliate.User.Core.MetalModels.UserPanel;
using System.Collections.Generic;

namespace Fanex.Affiliate.User.Core.Repository.UserPanel
{
    public interface IFeedbackRepository
    {
        ResultInfo Insert(FeedbackParam param);

        bool IsShowFeedback(int affId);

        IEnumerable<RateInfo> GetRateList();
    }
}
