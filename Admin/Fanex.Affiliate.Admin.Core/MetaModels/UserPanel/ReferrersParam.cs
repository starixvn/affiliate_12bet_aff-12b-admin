using System;

namespace Fanex.Affiliate.Admin.Core.MetaModels.UserPanel
{
    public class ReferrersParam
    {
        public int AffId { get; set; }

        public string AffName { get; set; }

        public int DeviceType { get; set; }

        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }
    }
}
