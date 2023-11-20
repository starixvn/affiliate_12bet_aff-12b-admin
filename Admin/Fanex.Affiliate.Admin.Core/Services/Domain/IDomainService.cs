using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Common;
using Fanex.Affiliate.Admin.Core.DTO.Domain;
using Fanex.Affiliate.Admin.Core.MetaModels.Domain;
using System.Collections.Generic;

namespace Fanex.Affiliate.Admin.Core.Services.Domain
{
    public interface IDomainService
    {
        IEnumerable<TargetCountry> GetTargetCountryList(TargetCountryParam param);

        IEnumerable<TargetCountry> GetVisibleTargetCountryListNotUsing(TargetCountryParam param);

        DomainInfo GetDomainById(int userId, int domainId, bool isAffDomain = true);

        IEnumerable<DomainInfo> GetDomainList(int userId, bool isAffDomain = true);

        ResultInfo InsertDomain(InsertDomainParam param);

        ResultInfo UpdateDomain(UpdateDomainParam param);

        ResultInfo DeleteDomain(DeleteDomainParam param);
    }
}
