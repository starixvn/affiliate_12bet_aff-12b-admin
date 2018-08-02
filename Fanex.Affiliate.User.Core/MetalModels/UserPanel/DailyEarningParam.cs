using System;
using Fanex.Data;

namespace Fanex.Affiliate.User.Core.MetalModels.UserPanel
{
    [Serializable]
    public class DailyEarningParam : BaseParam
    {
        public int AffId { get; set; }

        public byte? SportType { get; set; }

        public DateTime? MonthDate { get; set; }

        [SpParam(Ignored = true)]
        public bool IsOnlyContent { get; set; }
    }
}
