using Fanex.Affiliate.User.Core.MetalModels.BetList;

namespace Fanex.Affiliate.User.Core.Repository.BetList
{
    public interface IBetListRepository
    {
        bool IsValidMember(BetListValidationParam validationParam);
    }
}