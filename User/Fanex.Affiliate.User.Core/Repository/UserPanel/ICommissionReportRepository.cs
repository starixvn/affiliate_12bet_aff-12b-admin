using Fanex.Affiliate.User.Core.DTO.UserPanel;
using System;
using System.Collections.Generic;

namespace Fanex.Affiliate.User.Core.Repository.UserPanel
{
    public interface ICommissionReportRepository
    {
        IEnumerable<AffiliateCommission> GetAffiliateCommission(int affId, DateTime winlostdate);

    }
}
