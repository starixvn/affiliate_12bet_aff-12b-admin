using System;
using System.Collections.Generic;
using Fanex.Affiliate.User.Core.DTO;

namespace Fanex.Affiliate.User.Core.ViewModels.UserPanel
{
    public class DailyEarningDetailViewModel
    {
        public IEnumerable<SportTypeItem> SportTypeList { get; set; }

        public IEnumerable<DailyEarningDetail> MainData { get; set; }

        public int SelectedSportType { get; set; }

        public DateTime SelectedDate { get; set; }

        public bool HasDailyDetail { get; set; }
    }
}
