using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fanex.Affiliate.User.Core.DTO.UserPanel;

namespace Fanex.Affiliate.User.Core.Repository.UserPanel
{
    public class CommissionReportRepository : ICommissionReportRepository
    {
        public IEnumerable<AffiliateCommission> GetAffiliateCommission(int affId, DateTime winlostdate)
        {
            const string uSpKey = "Aff_User_Rpt_Commission";
            using (var objectDb = new ObjectDBBuilder(uSpKey))
            {
                return objectDb.DB.Query<AffiliateCommission>(new { affId, winlostdate });
            }
        }
    }
}
