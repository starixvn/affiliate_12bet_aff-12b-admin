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
        private readonly IObjectDBBuilder _builder;

        public CommissionReportRepository(IObjectDBBuilder builder)
        {
            _builder = builder;
        }

        public IEnumerable<AffiliateCommission> GetAffiliateCommission(int affId, DateTime winlostdate)
        {
            const string uSpKey = "Aff_User_Rpt_Commission";
            using (var db = _builder.Build(uSpKey))
            {
                return db.Query<AffiliateCommission>(new { affId, winlostdate });
            }
        }
    }
}
