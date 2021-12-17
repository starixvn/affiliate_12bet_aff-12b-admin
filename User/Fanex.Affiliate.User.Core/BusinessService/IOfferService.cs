using System.Threading.Tasks;

namespace Fanex.Affiliate.User.Core.BusinessService
{
    public interface IOfferService
    {
        Task<string> GetOfferById(int id);
    }
}
