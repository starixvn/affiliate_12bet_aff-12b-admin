using Fanex.Affiliate.User.Core.Repository;
using System;

namespace Fanex.Affiliate.User.Core.BusinessService
{
    public class IpAddressService : IIpAddressService
    {
        private readonly IIpAddressRepository _ipAddressRepository;

        public IpAddressService(IIpAddressRepository ipAddressRepository)
        {
            _ipAddressRepository = ipAddressRepository;
        }

        public bool IsThaiLandIpAddress(string ip)
        {
            var countryShort = _ipAddressRepository.GetContryShortByIpAddress(ip);

            return string.Equals("TH", countryShort, StringComparison.OrdinalIgnoreCase);
        }
    }
}
