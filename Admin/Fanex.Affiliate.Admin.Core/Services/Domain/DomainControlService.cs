using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Domain;
using Fanex.Affiliate.Admin.Core.MetaModels.Domain;
using Fanex.Affiliate.Admin.Core.Repository.Domain;
using System.Collections.Generic;

namespace Fanex.Affiliate.Admin.Core.Services.Domain
{
    public class DomainControlService : IDomainControlService
    {
        private readonly IDomainControlRepository _domainControlRepository;
        public DomainControlService(
            IDomainControlRepository domainControlRepository)
        {
            _domainControlRepository = domainControlRepository;
        }

        public IEnumerable<DomainControlInfo> GetDomainControlList(string targetCountryIds)
        {
            return _domainControlRepository.GetDomainControlList(targetCountryIds);
        }

        public DomainControlInfo GetDomainControlById(int domainControlId)
        {
            return _domainControlRepository.GetDomainControlById(domainControlId);
        }

        public ResultInfo InsertDomainControl(InsertDomainControlParam param)
        {
            return _domainControlRepository.InsertDomainControl(param);
        }

        public ResultInfo UpdateDomainControl(UpdateDomainControlParam param)
        {
            return _domainControlRepository.UpdateDomainControl(param);
        }

        public ResultInfo DeleteDomainControl(DeleteDomainControlParam param)
        {
            return _domainControlRepository.DeleteDomainControl(param);
        }
    }
}
