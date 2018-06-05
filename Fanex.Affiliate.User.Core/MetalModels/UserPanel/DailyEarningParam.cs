using System;

namespace Fanex.Affiliate.User.Core.MetalModels.UserPanel
{
    public class DailyEarningParam : BaseParam
    {
        public int AffId { get; set; }

        public byte SportType { get; set; }

        public DateTime MonthDate { get; set; }
    }
}
