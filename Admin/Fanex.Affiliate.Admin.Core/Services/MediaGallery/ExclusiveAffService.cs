using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.MediaGallery;
using Fanex.Affiliate.Admin.Core.MetaModels.MediaGallery;
using Fanex.Affiliate.Admin.Core.Repository.MediaGallery;

namespace Fanex.Affiliate.Admin.Core.Services.MediaGallery
{
    public class ExclusiveAffService : IExclusiveAffService
    {
        private readonly IExclusiveAffRepository _exclusiveAffiliateRepository;

        public ExclusiveAffService(IExclusiveAffRepository exclusiveAffiliateRepository)
        {
            _exclusiveAffiliateRepository = exclusiveAffiliateRepository;
        }

        public ResultInfo DeleteExclusiveAff(DeleteExclusiveAffParam param)
        {
            return _exclusiveAffiliateRepository.DeleteExclusiveAff(param);
        }

        public EntityResult<ExclusiveAff> GetExclusiveAffs(SelectExclusiveAffParam param)
        {
            return _exclusiveAffiliateRepository.GetExclusiveAffs(param);
        }

        public ResultInfo InsertExclusiveAff(InsertExclusiveAffParam param)
        {
            return _exclusiveAffiliateRepository.InsertExclusiveAff(param);
        }
    }
}
