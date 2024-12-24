using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Domain;
using Fanex.Affiliate.Admin.Core.MetaModels.Domain;
using System.Collections.Generic;

namespace Fanex.Affiliate.Admin.Core.Services.Domain
{
    public class SubAffDomainService : ISubAffDomainService
    {
        private readonly ICommonDomainService _commonDomainService;

        public SubAffDomainService(
            ICommonDomainService commonDomainService)
        {
            _commonDomainService = commonDomainService;
        }

        public IEnumerable<DomainInfo> GetSubAffDomainList()
        {
            const string SpKey = "Aff_Admin_SubAffiliateDomain_Sel";

            return _commonDomainService.GetDomainList(SpKey);
        }

        public ResultInfo InsertSubAffDomain(InsertUpdateDomainParam param)
        {
            const string SpKey = "Aff_Admin_SubAffiliateDomain_Ins";

            return _commonDomainService.InsertDomain(param, SpKey);
        }

        public ResultInfo UpdateSubAffDomain(InsertUpdateDomainParam param)
        {
            const string SpKey = "Aff_Admin_SubAffiliateDomain_Upd";

            return _commonDomainService.UpdateDomain(param, SpKey);
        }

        public ResultInfo DeleteSubAffDomain(DeleteDomainParam param)
        {
            const string SpKey = "Aff_Admin_SubAffiliateDomain_Del";

            return _commonDomainService.DeleteDomain(param, SpKey);
        }
    }
}
