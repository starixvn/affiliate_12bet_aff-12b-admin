using System;

namespace Fanex.Affiliate.Admin.Core.MetaModels.UserPanel
{
    public class DailyEarningParam
    {
        public int AffId { get; set; }

        public byte ProductId { get; set; }

        public DateTime MonthDate { get; set; }
    }
}
