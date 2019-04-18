using System;

namespace Fanex.Affiliate.User.Core.MetalModels.UserPanel
{
    public class MessageHistoryParam : BaseParam
    {
        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        public bool? Important { get; set; }

        public string Language { get; set; }

        public int PageSize { get; set; }

        public int PageIndex { get; set; }

        public int TotalPages { get; set; }

        public int Nrows { get; set; }
    }
}
