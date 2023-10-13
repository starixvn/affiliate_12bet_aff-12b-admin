using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Domain;
using Fanex.Affiliate.Admin.Core.MetaModels.Domain;
using Fanex.Affiliate.Admin.Core.Repository.Domain;
using System.Collections.Generic;
using System.Linq;

namespace Fanex.Affiliate.Admin.Core.Services.Domain
{
    public class DomainService : IDomainService
    {
        private const byte USING = 2;
        private readonly IDomainRepository _domainRepository;
        public DomainService(
            IDomainRepository domainRepository)
        {
            _domainRepository = domainRepository;
        }
        public IEnumerable<TargetCountry> GetTargetCountryList(TargetCountryParam param)
        {
            return _domainRepository.GetTargetCountryList(param);
        }

        public IEnumerable<TargetCountry> GetVisibleTargetCountriesByUser(int userId)
        {
            return GetTargetCountryList(new TargetCountryParam { UserId = userId });
        }

        public IEnumerable<TargetCountry> GetVisibleTargetCountriesNotUsingByUser(int userId)
        {
            return GetVisibleTargetCountriesByUser(userId).Where(m => m.Status != USING);
        }

        public IEnumerable<DomainInfo> GetDomainList(int userId, bool isAffDomain = true)
        {
            return _domainRepository.GetDomainList(userId, isAffDomain);
        }

        public DomainInfo GetDomainById(int userId, int domainId, bool isAffDomain = true)
        {
            return _domainRepository.GetDomainById(userId, domainId, isAffDomain);
        }

        public ResultInfo InsertDomain(InsertDomainParam param)
        {
            return _domainRepository.InsertDomain(param);
        }

        public ResultInfo UpdateDomain(UpdateDomainParam param)
        {
            return _domainRepository.UpdateDomain(param);
        }

        public ResultInfo DeleteDomain(DeleteDomainParam param)
        {
            return _domainRepository.DeleteDomain(param);
        }
    }
}
