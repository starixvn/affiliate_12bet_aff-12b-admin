namespace Fanex.Affiliate.User.Core.ViewModels.UserPanel
{
    using System.Collections.Generic;
    using Fanex.Affiliate.User.Core.DTO.UserPanel.AccountOverview;

    public class PlayerDetailViewModel
    {
        public IEnumerable<PlayerDetail> PlayerDetailList { get; set; }

        public int TotalItem { get; set; }

        public int StartRowNumber { get; set; }

        public bool IsDepositFeePercent { get; set; }

        public bool IsWithdrawalFeePercent { get; set; }
    }
}
