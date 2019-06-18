using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Affiliate;
using Fanex.Affiliate.Admin.Core.MetaModels.Affiliate;
using Fanex.Affiliate.Admin.Core.Repository;

namespace Fanex.Affiliate.Admin.Core.Services
{
    public class AffiliateService : IAffiliateService
    {
        private readonly IAffiliateRepository _affRepository;

        public AffiliateService(IAffiliateRepository affRepository)
        {
            _affRepository = affRepository;
        }

        public AffiliateBasicInfo GetAffiliateBasicInfo(int? affId, string affName = null)
        {
            return _affRepository.GetAffiliateBasicInfo(affId, affName);
        }

        public ResultInfo ApproveAffiliate(ApproveAffiliateParam param)
        {
            param.Status = "Approved";

            return _affRepository.ApproveAffiliate(param);
        }
    }
}
