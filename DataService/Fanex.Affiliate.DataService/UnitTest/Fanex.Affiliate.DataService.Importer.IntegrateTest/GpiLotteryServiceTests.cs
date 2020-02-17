using Fanex.Affiliate.DataService.Importer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            var service = new GpiLotteryService();
            var data = await service.GetRawDataListAsync(startTime, endTime);
            Assert.NotEmpty(data);
        }
    }
}
