using System;

namespace Fanex.Affiliate.Admin.Core.MetaModels.UserPanel
{
    public class ReferrersParam : AffiliateLinkParam
    {
        public int DeviceType { get; set; }

        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }
    }
}
