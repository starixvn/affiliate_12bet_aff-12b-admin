namespace Fanex.Affiliate.User.Core.CoreService
{
    using DTO;

    public interface IIpLocationService
    {
        IpLocationInfo GetCountryFromIp(string ip);
    }
}
