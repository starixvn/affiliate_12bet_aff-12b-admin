using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.UserPanel;
using Fanex.Affiliate.Admin.Core.MetaModels.UserPanel;
using System.Collections.Generic;

namespace Fanex.Affiliate.Admin.Core.Services.UserPanel.Statistics
{
    public interface IReferrersStatisticsService
    {
        IEnumerable<ReferrersStatistics> GetReferrersList(ReferrersParam param);

        EntityResult<ReferrersStatistics> GetReferrersDetailList(ReferrersDetailParam param);
    }
}
