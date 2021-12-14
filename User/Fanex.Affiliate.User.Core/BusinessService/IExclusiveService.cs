using System.Threading.Tasks;

namespace Fanex.Affiliate.User.Core.BusinessService
{
    public interface IExclusiveService
    {
        Task<string> GetExclusiveById(int id);
    }
}
