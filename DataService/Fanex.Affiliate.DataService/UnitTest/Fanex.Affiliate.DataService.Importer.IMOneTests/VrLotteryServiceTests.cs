using Fanex.Affiliate.DataService.Importer;
using Fanex.Affiliate.DataService.Importer.Core.Infrastructure;
using Fanex.Affiliate.DataService.Importer.Exceptions;
using Fanex.Affiliate.DataService.Importer.IMOne.Extension;
using Fanex.Affiliate.DataService.Importer.Repository;
using Fanex.Affiliate.DataService.Importer.Utilities;
using NSubstitute;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Fanex.Affiliate.DataService.Importer.IMOneTests
{
    public class TestGenerator 
    {
        public static IEnumerable<object[]> GenerateNotCorrectTimeRange()
        {
            yield return new object[] { new DateTime(2019, 1, 1, 12, 0, 1), new DateTime(2019,1,1,12,1,1) };
            yield return new object[] { new DateTime(2019, 1, 1, 12, 0, 0), new DateTime(2019, 1, 1, 10, 0, 0) };
            yield return new object[] { new DateTime(2019, 1, 1, 12, 0, 1), new DateTime(2019, 1, 1, 12, 59, 1) };
        }

        public static IEnumerable<object[]> GenerateCorrectTimeRange()
        {
            yield return new object[] { new DateTime(2019, 1, 1, 12, 0, 1), new DateTime(2019, 1, 1, 13, 0, 0) };
            yield return new object[] { new DateTime(2019, 1, 1, 12, 0, 1), new DateTime(2019, 1, 1, 15, 0, 0) };
            yield return new object[] { new DateTime(2018, 1, 1, 12, 0, 1), new DateTime(2019, 1, 1, 15, 0, 0) };
        }

        public static IEnumerable<object[]> GenerateResponseErrorCode()
        {
            yield return new object[] { @"{ ""Code"":""503"" }", new DateTime(2019, 1, 1, 12, 0, 1), new DateTime(2019, 1, 1, 13, 0, 0) };
            yield return new object[] { @"{ ""abc"":""503"" }", new DateTime(2019, 1, 1, 12, 0, 1), new DateTime(2019, 1, 1, 13, 0, 0) };
        }
        

    }
    public class VrLotteryServiceTests
    {
        public string SuccessReturn =>@"{""Code"":""0""}";
        private VrLotteryService CreateService(IApiClient client = null, IAppSettings appSetting = null, IThirdPartyProductRepository repo = null)
        {
            return new VrLotteryService(client, appSetting, repo);
        }

        [Theory]
        [MemberData(nameof(TestGenerator.GenerateNotCorrectTimeRange), MemberType = typeof(TestGenerator))]
        public void GetRawDataListByCurrency_WithNotCorrectTimeRange_ThrowException(DateTime startTime, DateTime endTime)
        {
            var service = CreateService();
            Assert.Throws<ArgumentException>(() => service.GetRawDataListByCurrency(startTime, endTime, "CNY").ToList());
        }

        [Theory]
        [MemberData(nameof(TestGenerator.GenerateCorrectTimeRange), MemberType = typeof(TestGenerator))]
        public void GetRawDataListByCurrency_WithGoodTimeRange_NotThrowException(DateTime startTime, DateTime endTime)
        {
            var stubApi = NSubstitute.Substitute.For<IApiClient>();
            var stubAppSetting = NSubstitute.Substitute.For<IAppSettings>();
            stubApi.GetResponse(Arg.Compat.Any<string>(), Arg.Compat.Any<NameValueCollection>())
                .ReturnsForAnyArgs(this.SuccessReturn);
            stubAppSetting.GetValue(Arg.Compat.Any<string>()).Returns("");
            var service = CreateService(stubApi, stubAppSetting);

            service.GetRawDataListByCurrency(startTime, endTime,"CNY").Count();

            Assert.True(true);
        }

        [Theory]
        [MemberData(nameof(TestGenerator.GenerateResponseErrorCode), MemberType = typeof(TestGenerator))]
        public void GetXmlDataList_WhenCall_ThrowException(string errorReturn, DateTime startTime, DateTime endTime)
        {
            var stubApi = NSubstitute.Substitute.For<IApiClient>();
            var stubAppSetting = NSubstitute.Substitute.For<IAppSettings>();

            stubApi.GetResponse(Arg.Compat.Any<string>(), Arg.Compat.Any<NameValueCollection>())
                .ReturnsForAnyArgs(errorReturn);
            stubAppSetting.GetValue(Arg.Compat.Any<string>()).Returns("");
            stubAppSetting.GetValue("VrLotteryCurrencies").Returns("CNC,BOB");

            var service = CreateService(stubApi, stubAppSetting);

            Assert.Throws<ThirdPartyProductException>(() => service.GetXmlDataList(startTime,endTime).Count());
        }

        [Fact]
        public void GetXmlDataList_WhenCall_NotThrowException()
        {
            var stubApi = NSubstitute.Substitute.For<IApiClient>();
            var stubAppSetting = NSubstitute.Substitute.For<IAppSettings>();
            var startTime = new DateTime(2019, 1, 1, 12, 0, 1);
            var endTime = new DateTime(2019, 1, 1, 13, 0, 0);

            stubApi.GetResponse(Arg.Compat.Any<string>(), Arg.Compat.Any<NameValueCollection>())
                .ReturnsForAnyArgs(this.SuccessReturn);
            stubAppSetting.GetValue(Arg.Compat.Any<string>()).Returns("");
            stubAppSetting.GetValue("VrLotteryCurrencies").Returns("CNC,BOB");
            var service = CreateService(stubApi, stubAppSetting);

            service.GetXmlDataList(startTime, endTime).Count();

            Assert.True(true);
        }
    }

}
