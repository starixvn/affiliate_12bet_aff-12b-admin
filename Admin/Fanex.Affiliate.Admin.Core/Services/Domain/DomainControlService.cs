using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Domain;
using Fanex.Affiliate.Admin.Core.MetaModels.Domain;
using Fanex.Affiliate.Admin.Core.Repository.Domain;
using System.Collections.Generic;

namespace Fanex.Affiliate.Admin.Core.Services.Domain
{
    public class DomainControlService : IDomainControlService
    {
        private readonly IDomainService _domainService;
        private readonly IDomainControlRepository _domainControlRepository;
        public DomainControlService(
            IDomainService domainService,
            IDomainControlRepository domainControlRepository)
        {
            _domainService = domainService;
            _domainControlRepository = domainControlRepository;
        }

        public IEnumerable<Market> GetVisibleMarketsByUser(int userId)
        {
            return _domainService.GetVisibleMarketsByUser(userId);
        }

        public IEnumerable<DomainControlInfo> GetDomainControlList(string marketIds)
        {
            return _domainControlRepository.GetDomainControlList(marketIds);
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
