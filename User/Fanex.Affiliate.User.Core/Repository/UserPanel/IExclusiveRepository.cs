using Fanex.Affiliate.User.Core.DTO.UserPanel;

namespace Fanex.Affiliate.User.Core.Repository.UserPanel
{
    public interface IExclusiveRepository
    {
        Exclusive GetExclusiveById(int exclusiveId);
    }
}
