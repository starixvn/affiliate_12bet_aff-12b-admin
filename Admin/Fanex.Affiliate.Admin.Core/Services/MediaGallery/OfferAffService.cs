using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.MediaGallery;
using Fanex.Affiliate.Admin.Core.MetaModels.MediaGallery;
using Fanex.Affiliate.Admin.Core.Repository.MediaGallery;

namespace Fanex.Affiliate.Admin.Core.Services.MediaGallery
{
    public class OfferAffService : IOfferAffService
    {
        private readonly IOfferAffRepository _OfferAffiliateRepository;

        public OfferAffService(IOfferAffRepository OfferAffiliateRepository)
        {
            _OfferAffiliateRepository = OfferAffiliateRepository;
        }

        public ResultInfo DeleteOfferAff(DeleteOfferAffParam param) => _OfferAffiliateRepository.DeleteOfferAff(param);

        public EntityResult<OfferAff> GetOfferAffs(SelectOfferAffParam param) => _OfferAffiliateRepository.GetOfferAffs(param);

        public ResultInfo InsertOfferAff(InsertOfferAffParam param)
        {
            return _OfferAffiliateRepository.InsertOfferAff(param);
        }
    }
}
