using System;

namespace Fanex.Affiliate.Admin.Core.MetaModels.UserPanel
{
    public class ViewLogParam
    {
        public int AffId { get; set; }

        public int LogType { get; set; }

        public string FromDate { get; set; }

        public string ToDate { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }
    }
}
