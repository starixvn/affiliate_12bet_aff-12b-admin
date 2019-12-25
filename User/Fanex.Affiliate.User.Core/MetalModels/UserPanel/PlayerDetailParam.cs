using Fanex.Affiliate.User.Core.Constants;
using System;

namespace Fanex.Affiliate.User.Core.MetalModels.UserPanel
{
    public class PlayerDetailParam
    {
        public int AffId { get; set; }

        public int PageIndex { get; set; } = Page.PageIndex;

        public int PageSize { get; set; } = Page.PageSize;

        public DateTime MonthDate { get; set; } = DateTime.Now;

        public int SortById { get; set; } = 1;

        public bool IsSortAsc { get; set; } = true;

        public int NRows { get; set; }
    }
}
