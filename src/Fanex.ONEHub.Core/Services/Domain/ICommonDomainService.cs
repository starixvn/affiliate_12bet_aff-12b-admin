using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Domain;
using Fanex.Affiliate.Admin.Core.MetaModels.Domain;
using System.Collections.Generic;

namespace Fanex.Affiliate.Admin.Core.Services.Domain
{
    public interface ICommonDomainService
    {
        IEnumerable<DomainInfo> GetDomainList(string spKey);

        ResultInfo InsertDomain(InsertDomainParam param, string spKey);

        ResultInfo UpdateDomain(UpdateDomainParam param, string spKey);

        ResultInfo DeleteDomain(DeleteDomainParam param, string spKey);

        IEnumerable<TargetCountryByDomain> GetTargetCountryListByDomain(int affDomainId = 0, int redirectDomainId = 0);
    }
}