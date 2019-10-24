using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Domain;
using Fanex.Affiliate.Admin.Core.MetaModels.Domain;
using Fanex.Affiliate.Admin.Core.Repository.Domain;
using System.Collections.Generic;

namespace Fanex.Affiliate.Admin.Core.Services.Domain
{
    public class DomainService : IDomainService
    {
        private readonly IDomainRepository _domainRepository;

        public DomainService(IDomainRepository domainRepository)
        {
            _domainRepository = domainRepository;
        }

        public IEnumerable<DomainInfo> GetDomainList()
        {
            return _domainRepository.GetDomainList();
        }

        public ResultInfo InsertDomain(InsertUpdateDomainParam param)
        {
            return _domainRepository.InsertDomain(param);
        }

        public ResultInfo UpdateDomain(InsertUpdateDomainParam param)
        {
            return _domainRepository.UpdateDomain(param);
        }

        public ResultInfo DeleteDomain(DeleteDomainParam param)
        {
            return _domainRepository.DeleteDomain(param);
        }
    }
}
