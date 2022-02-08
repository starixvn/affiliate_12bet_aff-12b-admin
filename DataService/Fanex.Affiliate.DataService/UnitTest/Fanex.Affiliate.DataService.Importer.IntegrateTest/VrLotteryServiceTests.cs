using Fanex.Affiliate.DataService.Importer.Core.Infrastructure;
using NSubstitute;
using System;
using System.Collections.Specialized;
using System.Net.Http;
using System.Threading;
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
            var successResponse = @"{ 'Code': 0, 'Pagination': { 'CurrentPage': 1, 'TotalPage': 1 } }";
            var stubApiClient = Substitute.For<IApiClientFactory>();
            stubApiClient.CreateApiClient(Arg.Compat.Any<string>(), Arg.Compat.Any<HttpMethod>(), Arg.Compat.Any<HttpClientHeaderOption>()).GetResponseAsync(Arg.Compat.Any<NameValueCollection>(), Arg.Compat.Any<CancellationToken>())
                .Returns(successResponse);
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
