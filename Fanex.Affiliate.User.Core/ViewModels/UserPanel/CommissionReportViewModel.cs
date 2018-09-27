using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fanex.Affiliate.User.Core.DTO.UserPanel;

namespace Fanex.Affiliate.User.Core.ViewModels.UserPanel
{
    public class CommissionReportViewModel
    {
        public IList<AffiliateCommisionByMonth> Data { get; set; }
        public AffiliateCommission Total { get; set; }
    }
}
