namespace Fanex.Affiliate.Link.Services.Impl
{
    using Fanex.Affiliate.Link.Criterias;
    using Fanex.Affiliate.Link.DTOs;
    using Fanex.Affiliate.Link.Services.Interface;
    using Fanex.Data.Repository;

    public class GeneralAffiliateInfoService : IGeneralAffiliateInfoService
    {
        private readonly IDynamicRepository _dynamicRepository;

        public GeneralAffiliateInfoService()
            : this(null)
        {
        }

        public GeneralAffiliateInfoService(IDynamicRepository dynamicRepository)
        {
            _dynamicRepository = dynamicRepository ?? new DynamicRepository();
        }

        public AffiliateInfo GetGeneralAffiliateInfo(int? affId = null, string affCode = null)
        {
            var criteria = new GeneralAffiliateInfoCriteria
            {
                AffId = affId,
                AffCode = affCode,
            };

            return _dynamicRepository.Get<AffiliateInfo>(criteria);
        }

        public CommGroup GetCommGroupByAffCode(string affCode)
        {
            var criteria = new CommGroupByAffCodeCriteria
            {
                AffCode = affCode,
            };

            return _dynamicRepository.Get<CommGroup>(criteria);
        }
    }
}
