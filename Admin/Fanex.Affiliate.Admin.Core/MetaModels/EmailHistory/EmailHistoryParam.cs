using System;

namespace Fanex.Affiliate.Admin.Core.MetaModels.EmailHistory
{
    public class EmailHistoryParam
    {
        public int? EmailId { get; set; }

        public bool HasGetByDate { get; set; }

        public string SenderName { get; set; }

        public string Email { get; set; }

        public string AffName { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }
    }
}
