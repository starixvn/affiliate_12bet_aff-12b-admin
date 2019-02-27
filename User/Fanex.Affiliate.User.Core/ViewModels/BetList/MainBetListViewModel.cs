using System;

namespace Fanex.Affiliate.User.Core.ViewModels.BetList
{
    public class MainBetListViewModel
    {
        public string BetListHtml { get; set; }

        public DateTime SelectedDate { get; set; }

        public int SelectedSportType { get; set; }

        public string UserName { get; set; }
    }
}
