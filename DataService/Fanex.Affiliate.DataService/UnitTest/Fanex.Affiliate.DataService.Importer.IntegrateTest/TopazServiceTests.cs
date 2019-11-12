using Fanex.Data.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Fanex.Affiliate.DataService.Importer.IntegrateTest
{
    public class TopazServiceTests
    {
        private TopazService CreateService()
        {
            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
            return new TopazService();
        }

        private int TimeDif(int elapse1, int elapse2)
        {
            if (elapse2 >= elapse1)
                return elapse2 - elapse1;
            else
                return int.MaxValue - elapse1 + elapse2 - int.MinValue;
        }

        [Theory]
        [InlineData(15)]
        [InlineData(31)]
        public void GetXmlDataList_WhenCall_ReturnNotNull(int minute)
        {
            var endTime = DateTime.UtcNow;
            //subtract 15 min;
            var startTime = endTime.AddMinutes(-minute);
            var service = CreateService();
            var dataList = service.GetXmlDataList(startTime, endTime);
            var count = dataList.Count();
            Assert.True(count > 0);
        }

        [Theory]
        [InlineData(15,2)]
        [InlineData(31,4)]
        public void GetXmlDataList_WhenCall_TimeToProcessLesserThanExpectTime(int minute, int expectTime)
        {
            var endTime = DateTime.UtcNow;
            //subtract 15 min;
            var startTime = endTime.AddMinutes(-minute);
            var service = CreateService();
            var maxTime = expectTime * 60 * 1000;
            var startCount = Environment.TickCount;

            var dataList = service.GetXmlDataList(startTime, endTime);
            var count = dataList.Count();
            var elapseTime = TimeDif(startCount, Environment.TickCount);

            Assert.InRange(elapseTime, maxTime - 120 * 1000, maxTime);
        }

        [Fact]
        public void Import_WhenCall_NotThrowException()
        {
            DbSettingProviderManager.Current.Start();
            var service = CreateService();
            var endTime = DateTime.UtcNow;
            var startTime = endTime.AddMinutes(-15);
            service.Import(startTime, endTime);
            Assert.True(true);
        }

        [Fact]
        public void GetXmlDataList_WhenCall_NotReturnEmpty()
        {
            var service = CreateService();
            var endTime = DateTime.UtcNow;
            var startTime = endTime.AddMinutes(-15);
            var list = service.GetXmlDataList(startTime, endTime).ToList();
            Assert.NotEmpty(list);
        }
    }
}
