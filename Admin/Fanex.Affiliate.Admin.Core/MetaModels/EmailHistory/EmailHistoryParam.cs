using System;

namespace Fanex.Affiliate.Admin.Core.MetaModels.EmailHistory
{
    public class EmailHistoryParam
    {
        public int SearchById { get; set; }

        public bool IsGetAll { get; set; }

        public string SearchText { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }
    }
}
