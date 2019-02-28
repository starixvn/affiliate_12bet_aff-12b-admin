using Fanex.Affiliate.Admin.Core.DTO.UserPanel;
using Fanex.Affiliate.Admin.Core.MetaModels.UserPanel;
using System.Collections.Generic;

namespace Fanex.Affiliate.Admin.Core.ViewModels.UserPanel
{
    public class TrackingStatisticsViewModel
    {
        public IEnumerable<string> TrackingCodeList { get; set; }

        public TrackingStatisticsParam TrackingStatisticsParam { get; set; }
    }
}
