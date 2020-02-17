using Fanex.Affiliate.DataService.Importer.Core.Infrastructure;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Fanex.Affiliate.DataService.Importer.IntegrateTest
{
    public class VrLotteryServiceTests
    {
        [Fact]
        public async Task GetRawDataList_WhenCallWithEnoughFieldAppsetting_NotThrowException()
        {
            var startTime = new DateTime(2019, 1, 1, 12, 0, 0);
            var endTime = new DateTime(2019, 1, 1, 13, 0, 0);
            var stubApiClient = NSubstitute.Substitute.For<IApiClient>();
            stubApiClient.GetResponseAsync(Arg.Compat.Any<string>(), Arg.Compat.Any<NameValueCollection>())
                .Returns("");
            var service = new VrLotteryService(stubApiClient);

            await service.GetRawDataListAsync(startTime, endTime);

            Assert.True(true);
        }

        [Fact]
        public async Task GetRawDataList_WhenCall_ResponseNotEmpty()
        {
            var startTime = new DateTime(2019, 10, 30, 12, 0, 0);
            var endTime = new DateTime(2019, 10, 30, 13, 0, 0);
            var service = new VrLotteryService();

            var data = await service.GetRawDataListAsync(startTime, endTime);

            Assert.NotEmpty(data);
        }
    }
}
