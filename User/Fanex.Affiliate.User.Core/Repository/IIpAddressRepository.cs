
namespace Fanex.Affiliate.User.Core.Repository
{
    public interface IIpAddressRepository
    {
        string GetContryShortByIpAddress(string ip);
    }
}
