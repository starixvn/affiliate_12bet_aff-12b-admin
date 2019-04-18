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

        public AffiliateBasicInfo GetAffiliateBasicInfo(int affId)
        {
            return _affRepository.GetAffiliateBasicInfo(affId);
        }

        public ResultInfo ApproveAffiliate(ApproveAffiliateParam param)
        {
            param.Status = "Approved";

            return _affRepository.ApproveAffiliate(param);
        }

        public AffiliateBasicInfo GetAffiliateBasicInfoByAffName(string affName)
        {
            return _affRepository.GetAffiliateBasicInfoByAffName(affName);
        }
    }
}
