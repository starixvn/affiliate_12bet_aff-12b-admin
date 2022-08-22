using Fanex.Affiliate.User.Core.DTO;
using Fanex.Affiliate.User.Core.DTO.UserPanel;
using Fanex.Affiliate.User.Core.MetalModels.UserPanel;
using Fanex.Affiliate.User.Core.Repository.UserPanel;
using Fanex.Affiliate.User.Core.Utilities;
using System;
using System.Collections.Generic;

namespace Fanex.Affiliate.User.Core.BusinessService
{
    public class FeedbackService : IFeedbackService
    {
        private readonly IAppSetting _appSetting;
        private readonly IFeedbackRepository _feedbackRepository;

        public FeedbackService(
            IAppSetting appSetting, 
            IFeedbackRepository feedbackRepository)
        {
            _appSetting = appSetting;
            _feedbackRepository = feedbackRepository;
        }

        public IEnumerable<RateInfo> GetRateList()
        {
            return _feedbackRepository.GetRateList();
        }

        public ResultInfo Insert(FeedbackParam param)
        {
            return _feedbackRepository.Insert(param);
        }

        public bool IsValidAffToShowFeedback(int affId, DateTime? lastestRequestTime = null)
        {
            if(lastestRequestTime is null)
            {
                return _feedbackRepository.IsShowFeedback(affId);
            }

            var isValidIdleTime = DateTime.Compare(lastestRequestTime.Value.AddMinutes(_appSetting.ValidIdleTime), DateTime.Now) > 0;

            return isValidIdleTime && _feedbackRepository.IsShowFeedback(affId);
        }
    }
}
