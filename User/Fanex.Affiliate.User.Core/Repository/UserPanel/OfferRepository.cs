using Fanex.Affiliate.User.Core.DTO.UserPanel;

namespace Fanex.Affiliate.User.Core.Repository.UserPanel
{
    public class OfferRepository : IOfferRepository
    {
        private readonly IObjectDBBuilder _builder;

        public OfferRepository(IObjectDBBuilder builder)
        {
            _builder = builder;
        }

        public Offer GetOfferById(int pageId)
        {
            const string spKey = "Aff_Admin_OfferPage_Sel";
            using (var objectDb = _builder.Build(spKey))
            {
                return objectDb.QueryEntity<Offer>(new { pageId });
            }
        }
    }
}
