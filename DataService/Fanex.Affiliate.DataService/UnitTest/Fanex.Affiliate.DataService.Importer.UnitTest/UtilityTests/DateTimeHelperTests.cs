using Fanex.Affiliate.DataService.Importer.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Fanex.Affiliate.DataService.Importer.UnitTest.UtilityTests
{
    public class DateTimeHelperTests
    {
        [Fact]
        public void GetProportionTimeRange_WhenCallWithZeroSpan_ThrowException()
        {
            var startTime = new DateTime(2019, 10, 10, 10, 0, 0);
            var endTime = new DateTime(2019, 10, 10, 10, 10, 0);
            var span = TimeSpan.Zero;
            Assert.Throws<Exception>(() => DateTimeHelper.GetProportionTimeRange(startTime, endTime, span).Count());
        }

        [Fact]
        public void GetPropotionTimeRange_WhenCallWithStartTimeLesserThanEndTime_ReturnEmpty()
        {
            var startTime = new DateTime(2019, 10, 10, 10, 10, 0);
            var endTime = new DateTime(2019, 10, 10, 10, 0, 0);
            var span = new TimeSpan(0,10,0);
            var ranges = DateTimeHelper.GetProportionTimeRange(startTime, endTime, span);
            Assert.Empty(ranges);
        }

        [Fact]
        public void GetPropotionTimeRange_WhenCall_NotTakeTimeRangeNotEqualToSpan()
        {
            var startTime = new DateTime(2019, 10, 10, 10, 0, 0);
            var endTime = new DateTime(2019, 10, 10, 10, 55, 0);
            var span = new TimeSpan(0, 10, 0);
            var ranges = DateTimeHelper.GetProportionTimeRange(startTime, endTime, span, true);
            Assert.Equal(5, ranges.Count());
        }

        [Fact]
        public void GetPropotionTimeRange_WhenCall_TakeTimeRangeEqualToSpan()
        {
            var startTime = new DateTime(2019, 10, 10, 10, 0, 0);
            var endTime = new DateTime(2019, 10, 10, 10, 55, 0);
            var span = new TimeSpan(0, 10, 0);
            var ranges = DateTimeHelper.GetProportionTimeRange(startTime, endTime, span, false);
            Assert.Equal(6, ranges.Count());
        }
    }
}
