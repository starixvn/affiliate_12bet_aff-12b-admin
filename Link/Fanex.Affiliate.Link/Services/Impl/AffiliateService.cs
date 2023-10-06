using Fanex.Affiliate.Link.DataAccess.Impl;
using Fanex.Affiliate.Link.DataAccess.Interface;
using Fanex.Affiliate.Link.Services.Interface;

namespace Fanex.Affiliate.Link.Services.Impl
{
    public class AffiliateService : IAffiliateService
    {
        private readonly IAffiliateDataAccess _affiliateDataAccess;

        public AffiliateService() : this(null)
        {
        }

        private AffiliateService(IAffiliateDataAccess affiliateDataAccess)
        {
            _affiliateDataAccess = affiliateDataAccess ?? new AffiliateDataAccess();
        }

        public string GetCommGroupByAffId(int affId)
        {
            return _affiliateDataAccess.GetCommGroupByAffId(affId);
        }
    }
}