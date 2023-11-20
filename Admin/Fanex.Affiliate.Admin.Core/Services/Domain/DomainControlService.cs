using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Common;
using Fanex.Affiliate.Admin.Core.DTO.Domain;
using Fanex.Affiliate.Admin.Core.Enum;
using Fanex.Affiliate.Admin.Core.MetaModels.Domain;
using Fanex.Affiliate.Admin.Core.Repository.Domain;
using System.Collections.Generic;
using System.Linq;

namespace Fanex.Affiliate.Admin.Core.Services.Domain
{
    public class DomainControlService : IDomainControlService
    {
        private readonly IDomainControlRepository _domainControlRepository;
        private readonly IDomainService _domainService;
        public DomainControlService(
            IDomainControlRepository domainControlRepository,
            IDomainService domainService)
        {
            _domainService = domainService;
            _domainControlRepository = domainControlRepository;
        }

        public IEnumerable<TargetCountry> GetVisibleTargetCountryListNotBlocked(TargetCountryParam param)
        {
            return _domainService.GetTargetCountryList(param)?.Where(m => m.Status != (byte)TargetCountryStatus.Blocked);
        }

        public IEnumerable<DomainControlInfo> GetDomainControlList(string targetCountryCodes)
        {
            return _domainControlRepository.GetDomainControlList(targetCountryCodes);
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
