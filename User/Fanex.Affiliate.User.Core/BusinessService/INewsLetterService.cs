using System.Threading.Tasks;

namespace Fanex.Affiliate.User.Core.BusinessService
{
    public interface INewsLetterService
    {
        Task<string> GetCurrentContent();

        Task<string> GetContentById(int id);
    }
}
