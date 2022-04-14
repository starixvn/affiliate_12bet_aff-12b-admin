using Fanex.Affiliate.DataService.Importer.Core.Infrastructure;
using NSubstitute;
using System;
using System.Collections.Specialized;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Fanex.Affiliate.DataService.Importer.IntegrateTest
{
    public class LBKenoServiceTests
    {
        [Fact]
        public async Task GetRawDataList_WhenCall_NotEmptyResponse()
        {
            var successResponse = @"<game_transaction_detail>
                                      <status_code>00</status_code>
                                      <status_text>OK</status_text>
                                      <bet>
                                        <item  bet_time=""2019-11-06 22:47:05"" trans_time=""2019-11-06 22:47:00""/>
                                      </bet >
                                   </game_transaction_detail>";
            var client = Substitute.For<IApiClientFactory>();
            client.CreateApiClient(Arg.Compat.Any<string>()).GetResponseAsync(Arg.Compat.Any<NameValueCollection>(), Arg.Compat.Any<CancellationToken>())
                .Returns(successResponse);

            var service = new LBKenoService(client);
            var startDate = new DateTime(2019, 10, 30);
            var endDate = new DateTime(2019, 10, 31);
            var data = await service.GetRawDataListAsync(startDate, endDate);
            Assert.NotEmpty(data);
        }
    }
}
