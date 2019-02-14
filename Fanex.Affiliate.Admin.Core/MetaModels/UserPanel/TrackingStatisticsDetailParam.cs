using System;

namespace Fanex.Affiliate.Admin.Core.MetaModels.UserPanel
{
    public class TrackingStatisticsDetailParam : TrackingStatisticsParam
    {
        public DateTime SelectedMonth { get; set; } = DateTime.Now;
    }
}
