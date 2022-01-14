using Fanex.Affiliate.User.Core.DTO.UserPanel;

namespace Fanex.Affiliate.User.Core.Repository.UserPanel
{
    public interface IOfferRepository
    {
        Offer GetOfferById(int pageId);
    }
}
