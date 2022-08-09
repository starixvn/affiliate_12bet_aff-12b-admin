using Fanex.Affiliate.User.Core.DTO;
using Fanex.Affiliate.User.Core.MetalModels.UserPanel;
using Fanex.Affiliate.User.Core.Repository.UserPanel;

namespace Fanex.Affiliate.User.Core.BusinessService
{
    public class FeedbackService : IFeedbackService
    {
        private readonly IFeedbackRepository _feedbackRepository;

        public FeedbackService(IFeedbackRepository feedbackRepository)
        {
            _feedbackRepository = feedbackRepository;
        }

        public ResultInfo Insert(FeedbackParam param)
        {
            return _feedbackRepository.Insert(param);
        }

        public bool IsShowFeedback(int affId)
        {
            return _feedbackRepository.IsShowFeedback(affId);
        }

        public void CloseFeedback(int affId)
        {
            _feedbackRepository.CloseFeedback(affId);
        }
    }
}
