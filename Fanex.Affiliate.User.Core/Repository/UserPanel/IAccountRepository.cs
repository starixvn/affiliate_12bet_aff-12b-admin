using Fanex.Affiliate.User.Core.ViewModels;

namespace Fanex.Affiliate.User.Core.Repository.UserPanel
{
    public interface IAccountRepository
    {
        AccountInformationViewModel GetAccountInfo(int affId);
    }
}