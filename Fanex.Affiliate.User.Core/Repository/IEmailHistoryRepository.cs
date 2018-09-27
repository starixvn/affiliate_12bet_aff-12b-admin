using Fanex.Affiliate.User.Core.DTO;
using Fanex.Affiliate.User.Core.MetalModels;

namespace Fanex.Affiliate.User.Core.Repository
{
    public interface IEmailHistoryRepository
    {
        ResultInfo Insert(ContactUsParam param);
    }
}
