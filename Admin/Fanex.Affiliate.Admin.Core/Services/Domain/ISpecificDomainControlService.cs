using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Domain;
using Fanex.Affiliate.Admin.Core.MetaModels.Domain;
using System.Collections.Generic;

namespace Fanex.Affiliate.Admin.Core.Services.Domain
{
    public interface ISpecificDomainControlService
    {
        IEnumerable<SpecificDomainControlInfo> GetSpecificDomainControl(SpecificDomainControlParam param);

        SpecificDomainControlInfo GetSpecificDomainControlById(int id);

        ResultInfo InsertSpecificDomainControl(InsertSpecificDomainControlParam param);

        ResultInfo UpdateSpecificDomainControl(UpdateSpecificDomainControlParam param);

        ResultInfo DeleteSpecificDomainControl(DeleteDomainControlParam param);
    }
}
