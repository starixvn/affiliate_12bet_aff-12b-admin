using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Domain;
using Fanex.Affiliate.Admin.Core.MetaModels.Domain;
using System.Collections.Generic;

namespace Fanex.Affiliate.Admin.Core.Services.Domain
{
    public interface IDomainControlService
    {
        IEnumerable<Market> GetVisibleMarketsByUser(int userId);

        IEnumerable<DomainControlInfo> GetDomainControlList(string marketIds);

        DomainControlInfo GetDomainControlById(int domainControlId);

        ResultInfo InsertDomainControl(InsertDomainControlParam param);

        ResultInfo UpdateDomainControl(UpdateDomainControlParam param);

        ResultInfo DeleteDomainControl(DeleteDomainControlParam param);
    }
}
