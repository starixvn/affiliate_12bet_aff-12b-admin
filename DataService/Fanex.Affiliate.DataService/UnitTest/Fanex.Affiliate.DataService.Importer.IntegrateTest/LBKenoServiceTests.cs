using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Fanex.Affiliate.DataService.Importer.IntegrateTest
{
    public class LBKenoServiceTests
    {
        [Fact]
        public async Task GetRawDataList_WhenCall_NotEmptyResponse()
        {
            var service = new LBKenoService();
            var startDate = new DateTime(2019, 10, 30);
            var endDate = new DateTime(2019, 10, 31);
            var data = await service.GetRawDataListAsync(startDate, endDate);
            Assert.NotEmpty(data);

        }
    }
}
