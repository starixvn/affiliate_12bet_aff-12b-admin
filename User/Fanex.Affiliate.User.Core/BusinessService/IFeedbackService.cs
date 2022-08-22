using Fanex.Affiliate.User.Core.DTO;
using Fanex.Affiliate.User.Core.DTO.UserPanel;
using Fanex.Affiliate.User.Core.MetalModels.UserPanel;
using System;
using System.Collections.Generic;

namespace Fanex.Affiliate.User.Core.BusinessService
{
    public interface IFeedbackService
    {
        IEnumerable<RateInfo> GetRateList();

        ResultInfo Insert(FeedbackParam param);

        bool IsValidAffToShowFeedback(int affId, DateTime? lastestRequestTime = null);
    }
}
