using System;

namespace Fanex.Affiliate.Admin.Core.MetaModels.UserPanel
{
    public class ReferrersParam
    {
        public int AffId { get; set; }

        public int DeviceType { get; set; }

        public string FromDate { get; set; } = $"{DateTime.Now.Month}/01/{DateTime.Now.Year}";

        public string ToDate { get; set; } = DateTime.Now.ToString("MM/dd/yyyy");
    }
}
