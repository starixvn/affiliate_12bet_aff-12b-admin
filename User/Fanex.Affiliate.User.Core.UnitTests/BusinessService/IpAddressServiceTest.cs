using Fanex.Affiliate.User.Core.BusinessService;
using Fanex.Affiliate.User.Core.Repository;
using NSubstitute;
using Xunit;

namespace Fanex.Affiliate.User.Core.UnitTests.BusinessService
{
    public class IpAddressServiceTest
    {
        private readonly IIpAddressRepository _IpAddressRepository;

        public IpAddressServiceTest()
        {
            _IpAddressRepository = Substitute.For<IIpAddressRepository>();
        }

        [Fact]
        public void IsThaiLandIpAddress_WhenCall_CallRepo()
        {
            const string ip = "127.0.0.1";
            var service = CreateService();

            service.IsThaiLandIpAddress(ip);

            _IpAddressRepository.Received().GetContryShortByIpAddress(ip);
        }

        private IpAddressService CreateService()
            => new IpAddressService(_IpAddressRepository);
    }
}
