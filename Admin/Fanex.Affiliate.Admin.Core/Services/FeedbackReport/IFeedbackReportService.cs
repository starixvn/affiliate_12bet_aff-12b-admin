using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.FeedbackReport;
using Fanex.Affiliate.Admin.Core.MetaModels.FeedbackReport;
using System.Collections.Generic;

namespace Fanex.Affiliate.Admin.Core.Services.FeedbackReport
{
    public interface IFeedbackReportService
    {
        IEnumerable<RateInfo> GetRateList();

        EntityResult<FeedbackReportInfo> GetFeedbackReportList(FeedbackReportParam param);
    }
}
