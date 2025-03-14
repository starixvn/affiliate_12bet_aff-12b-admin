using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Domain;
using Fanex.Affiliate.Admin.Core.MetaModels.Domain;
using System.Collections.Generic;

namespace Fanex.Affiliate.Admin.Core.Services.Domain
{
    public interface IDomainControlService
    {
        IEnumerable<Market> GetMarketListByDomain(int affDomainId = 0, int redirectDomainId = 0);

        IEnumerable<DomainControlInfo> GetDomainControl(string market, int domainControlId = 0);

        DomainControlInfo GetDomainControlById(int domainControlId);

        ResultInfo InsertDomainControl(InsertDomainControlParam param);

        ResultInfo UpdateDomainControl(UpdateDomainControlParam param);

        ResultInfo DeleteDomainControl(DeleteDomainControlParam param);

        IEnumerable<string> GetSubAffDomainControlList();
    }
}
