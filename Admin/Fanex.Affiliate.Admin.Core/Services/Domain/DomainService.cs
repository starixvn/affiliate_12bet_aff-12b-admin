namespace Fanex.Affiliate.Admin.Core.Services.Domain
{
    using DTO;
    using Fanex.Affiliate.Admin.Core.DTO.Common;
    using Fanex.Affiliate.Admin.Core.DTO.Domain;
    using Fanex.Affiliate.Admin.Core.MetaModels.Domain;
    using System.Collections.Generic;
    using System.Linq;
    using Utilities.Market;

    public class DomainService : IDomainService
    {
        private readonly ICommonDomainService _commonDomainService;

        public DomainService(
            ICommonDomainService commonDomainService)
        {
            _commonDomainService = commonDomainService;
        }

        public IEnumerable<TargetCountryByDomain> GetTargetCountryListByDomain(int affDomainId = 0, int redirectDomainId = 0)
            => _commonDomainService.GetTargetCountryListByDomain(affDomainId, redirectDomainId);

        private static string GetDomainSpKey(bool isAffDomain) =>
            isAffDomain ? "Aff_Admin_AffiliateDomain_Sel" : "Aff_Admin_LicenseeDomain_Sel";

        public IEnumerable<DomainInfo> GetDomainList(bool isAffDomain = true)
        {
            return _commonDomainService.GetDomainList(GetDomainSpKey(isAffDomain));
        }

        public DomainInfo GetDomainById(int domainId, bool isAffDomain = true)
        {
            return _commonDomainService.GetDomainList(GetDomainSpKey(isAffDomain)).FirstOrDefault(d => d.DomainId == domainId);
        }

        public ResultInfo InsertDomain(InsertDomainParam param)
        {
            var spKey = param.IsAffDomain ? "Aff_Admin_AffiliateDomain_Ins" : "Aff_Admin_LicenseeDomain_Ins";

            return _commonDomainService.InsertDomain(param, spKey);
        }

        public ResultInfo UpdateDomain(UpdateDomainParam param)
        {
            var spKey = param.IsAffDomain ? "Aff_Admin_AffiliateDomain_Upd" : "Aff_Admin_LicenseeDomain_Upd";

            return _commonDomainService.UpdateDomain(param, spKey);
        }

        public ResultInfo DeleteDomain(DeleteDomainParam param)
        {
            var spKey = param.IsAffDomain ? "Aff_Admin_AffiliateDomain_Del" : "Aff_Admin_LicenseeDomain_Del";

            return _commonDomainService.DeleteDomain(param, spKey);
        }
    }
}
