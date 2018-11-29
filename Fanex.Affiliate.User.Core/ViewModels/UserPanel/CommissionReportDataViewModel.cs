using System.Collections.Generic;
using Fanex.Affiliate.User.Core.DTO.UserPanel;

namespace Fanex.Affiliate.User.Core.ViewModels.UserPanel
{
    public class CommissionReportDataViewModel
    {
        public IList<AffiliateCommisionByMonth> Data { get; set; }
        public AffiliateCommission Total { get; set; }
    }
}
