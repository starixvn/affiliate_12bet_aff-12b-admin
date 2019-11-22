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
        public void GetRawDataList_WhenCallWithEnoughFieldAppsetting_NotThrowException()
        {
            var startTime = new DateTime(2019, 1, 1, 12, 0, 0);
            var endTime = new DateTime(2019, 1, 1, 13, 0, 0);
            var stubApiClient = NSubstitute.Substitute.For<IApiClient>();
            stubApiClient.GetResponse(Arg.Compat.Any<string>(), Arg.Compat.Any<NameValueCollection>())
                .Returns("");
            var service = new VrLotteryService(stubApiClient);

            service.GetRawDataListAsync(startTime, endTime).Result.ToList();

            Assert.True(true);
        }

        [Fact]
        public void GetRawDataList_WhenCall_ResponseNotEmpty()
        {
            var startTime = new DateTime(2019, 10, 30, 12, 0, 0);
            var endTime = new DateTime(2019, 10, 30, 13, 0, 0);
            var service = new VrLotteryService();

            var data = service.GetRawDataListAsync(startTime, endTime).Result.ToList();

            Assert.NotEmpty(data);
        }
    }
}
