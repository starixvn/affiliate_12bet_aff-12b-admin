using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fanex.Affiliate.User.Core.DTO.UserPanel;

namespace Fanex.Affiliate.User.Core.Repository.UserPanel
{
    public interface ICommissionReportRepository
    {
        IEnumerable<AffiliateCommission> GetAffiliateCommission(int affId, DateTime winlostdate);
            
    }
}
