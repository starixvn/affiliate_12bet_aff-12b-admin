using System;

namespace Fanex.Affiliate.User.Core.MetalModels.UserPanel
{
    public class MessageHistoryParam : BaseParam
    {
        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        public bool? Important { get; set; }

        public string Language { get; set; }

        public int PageSize { get; set; } = 10;

        public int PageIndex { get; set; } = 1;

        public int TotalPages { get; set; }

        public int Nrows { get; set; }
    }
}
