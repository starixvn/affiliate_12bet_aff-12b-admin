using Fanex.Affiliate.DataService.Importer;
using Fanex.Affiliate.DataService.Importer.Core.Infrastructure;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Fanex.Affiliate.DataService.Importer.IntegrateTest
{
    public class GpiLotteryServiceTests
    {
        [Fact]
        public async Task GetXmlDataList_WhenCall_ResponseNotNull()
        {
            var startTime = new DateTime(2019, 10, 30, 0, 0, 0);
            var endTime = new DateTime(2019, 10, 31, 0, 0, 0);
            var successResponse = @"[{ 'Code': 1 }]";
            var stubClientFactory = Substitute.For<IApiClientFactory>();
            stubClientFactory.CreateApiClient(Arg.Compat.Any<string>(), Arg.Compat.Any<HttpMethod>(), Arg.Compat.Any<HttpClientHeaderOption>()).GetResponseAsync(Arg.Compat.Any<NameValueCollection>(), Arg.Compat.Any<CancellationToken>())
                .Returns(successResponse);
            var service = new GpiLotteryService(stubClientFactory);
            var data = await service.GetRawDataListAsync(startTime, endTime);
            Assert.NotEmpty(data);
        }
    }
}
