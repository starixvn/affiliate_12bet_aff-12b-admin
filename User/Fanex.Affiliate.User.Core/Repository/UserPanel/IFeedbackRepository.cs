using Fanex.Affiliate.User.Core.DTO;
using Fanex.Affiliate.User.Core.MetalModels.UserPanel;

namespace Fanex.Affiliate.User.Core.Repository.UserPanel
{
    public interface IFeedbackRepository
    {
        ResultInfo Insert(FeedbackParam param);

        bool IsShowFeedback(int affId);

        void CloseFeedback(int affId);
    }
}
