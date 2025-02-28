namespace Fanex.Affiliate.User.Core.MetalModels.UserPanel
{
    using Constants;
    using System;
    using Utilities;

    public class PlayerDetailParam
    {
        private const int Turnover = 5;

        public int AffId { get; set; }

        public int PageIndex { get; set; } = Page.PageIndex;

        public int PageSize { get; set; } = Page.PageSize;

        public DateTime MonthDate { get; set; } = DateTimeHelper.Now;

        public int SortById { get; set; } = Turnover;

        public bool IsSortAsc { get; set; }

        public int NRows { get; set; }
    }
}
