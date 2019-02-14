using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.UserPanel;
using Fanex.Affiliate.Admin.Core.MetaModels.UserPanel;
using System.Collections.Generic;

namespace Fanex.Affiliate.Admin.Core.Repository.UserPanel
{
    public interface IReferrersRepository
    {
        IEnumerable<Referrers> GetReferrersAllSites(ReferrersParam param);

        EntityResult<Referrers> GetReferrersBySite(ReferrersDetailParam param);
    }
}
