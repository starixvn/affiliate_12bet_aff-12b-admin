using Fanex.Affiliate.DataService.Importer.Gpi.Enum;
using Fanex.Affiliate.DataService.Importer.Gpi.Utilities;
using Xunit;


namespace Fanex.Affiliate.DataService.Importer.UnitTest
{

    public class GPIServiceTests
    {
        private GpiLotteryService CreateGpiLotteryService()
        {
            return new GpiLotteryService();
        }

        [Theory]
        [InlineData(GameType.Keno, "keno")]
        [InlineData(GameType.Fast3, "fast3")]
        [InlineData(GameType.Pk10, "pk10")]
        public void CheckEnumName_GameType_ReturnTrue(GameType type, string expected)
        {
            Assert.Contains(expected, GpiHelper.GetGameTypeName(type));
        }


    }
}
