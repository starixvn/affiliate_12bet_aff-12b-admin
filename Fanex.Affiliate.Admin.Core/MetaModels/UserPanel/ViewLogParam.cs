using System;

namespace Fanex.Affiliate.Admin.Core.MetaModels.UserPanel
{
    public class ViewLogParam
    {
        public int AffId { get; set; }

        public int LogType { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }
    }
}
