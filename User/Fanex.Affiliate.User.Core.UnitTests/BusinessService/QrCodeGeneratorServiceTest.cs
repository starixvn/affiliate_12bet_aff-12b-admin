using Fanex.Affiliate.User.Core.BusinessService;
using Fanex.Affiliate.User.Core.UnitTests.TestHelper;
using Xunit;

namespace Fanex.Affiliate.User.Core.UnitTests.BusinessService
{
    public class QrCodeGeneratorServiceTest
    {
        [Fact]
        public void GenerateBase64QrCode_WhenNeedPrefixForWeb_ReturnResultHavePrefix()
        {
            var service = CreateService();

            var result = service.GenerateBase64QrCode("abcd", string.Empty, true);

            Assert.Contains("data:image/png;base64,", result);
        }

        [Fact]
        public void GenerateBase64QrCode_WhenCallWithIcon_ReturnNotEmpty()
        {
            var icon = Constant.LogoImageBase64;
            var service = CreateService();

            var result = service.GenerateBase64QrCode("abcd", icon, false);

            Assert.NotEmpty(result);
        }

        private static QrCodeGeneratorService CreateService()
            => new QrCodeGeneratorService();
    }
}
