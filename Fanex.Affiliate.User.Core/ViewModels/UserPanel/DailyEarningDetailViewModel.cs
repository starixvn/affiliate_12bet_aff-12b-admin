using System;
using System.Collections.Generic;
using Fanex.Affiliate.User.Core.DTO;

namespace Fanex.Affiliate.User.Core.ViewModels.UserPanel
{
    public class DailyEarningDetailViewModel
    {
        public IEnumerable<ProductItem> ProductList { get; set; }

        public IEnumerable<DailyEarningDetail> MainData { get; set; }

        public int SelectedProduct { get; set; }

        public DateTime SelectedDate { get; set; }

        public bool HasDailyDetail { get; set; }
    }
}
