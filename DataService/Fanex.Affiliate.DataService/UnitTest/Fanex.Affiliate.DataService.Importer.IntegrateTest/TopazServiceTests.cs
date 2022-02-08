
using Fanex.Affiliate.DataService.Importer.Core.Infrastructure;
using Fanex.Data;
using Fanex.Data.SqlServer;
using NSubstitute;
using System;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Fanex.Affiliate.DataService.Importer.IntegrateTest
{
    public class TopazServiceTests
    {
        public static string RESPONSE_SUCCESS => @"{'status':'SUCCESS', 'access_token': 'test'}";

        private TopazService CreateService()
        {
            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);

            var stubApiClient = Substitute.For<IApiClientFactory>();
            stubApiClient.CreateApiClient(Arg.Compat.Any<string>(), Arg.Compat.Any<HttpMethod>(), Arg.Compat.Any<HttpClientHeaderOption>()).GetResponseAsync(default(NameValueCollection), default(CancellationToken))
                .ReturnsForAnyArgs(RESPONSE_SUCCESS);

            return new TopazService(stubApiClient);
        }

        private int TimeDif(int elapse1, int elapse2)
        {
            if (elapse2 >= elapse1)
                return elapse2 - elapse1;
            else
                return int.MaxValue - elapse1 + elapse2 - int.MinValue;
        }

        [Theory]
        [InlineData(60)]
        [InlineData(120)]
        public async Task GetXmlDataList_WhenCall_ReturnNotNull(int minute)
        {
            var endTime = DateTime.UtcNow;

            var startTime = endTime.AddMinutes(-minute);
            var service = CreateService();
            var dataList = await service.GetXmlDataListAsync(startTime, endTime);
            var count = dataList.Count();
            Assert.True(count > 0);
        }

        [Theory]
        [InlineData(60,2)]
        //[InlineData(120,4)]
        public async Task GetXmlDataList_WhenCall_TimeToProcessLesserThanExpectTime(int minute, int expectTime)
        {
            var endTime = DateTime.UtcNow;

            var startTime = endTime.AddMinutes(-minute);
            var service = CreateService();
            var maxTime = expectTime * 60 * 1000;
            var startCount = Environment.TickCount;

            await service.GetXmlDataListAsync(startTime, endTime);
            var elapseTime = TimeDif(startCount, Environment.TickCount);

            var lowTime = maxTime - 120 * 1000;
            Assert.InRange(elapseTime, lowTime, maxTime);
        }

        [Fact]
        public async Task Import_WhenCall_NotThrowException()
        {
            DbSettingProviderManager
                .StartNewSession()
                .WithSqlServer("~/App_Data")
                .Run();
            var service = CreateService();
            var endTime = DateTime.UtcNow;
            var startTime = endTime.AddMinutes(-15);
            await service.ImportAsync(startTime, endTime);
            Assert.True(true);
        }

        [Fact]
        public async Task GetXmlDataList_WhenCall_NotReturnEmpty()
        {
            var service = CreateService();
            var endTime = DateTime.UtcNow;
            var startTime = endTime.AddMinutes(-60);
            var list = await service.GetXmlDataListAsync(startTime, endTime);
            Assert.NotEmpty(list);
        }
    }
}
