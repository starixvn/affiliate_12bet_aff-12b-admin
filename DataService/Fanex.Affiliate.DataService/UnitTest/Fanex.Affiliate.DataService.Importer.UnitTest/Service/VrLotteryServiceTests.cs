using Fanex.Affiliate.DataService.Importer.Core.Infrastructure;
using Fanex.Affiliate.DataService.Importer.Core.Exceptions;
using Fanex.Affiliate.DataService.Importer.Core.Repository;
using Fanex.Affiliate.DataService.Importer.Core.Utilities;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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
        public async Task GetRawDataListByCurrency_WithNotCorrectTimeRange_ThrowException(DateTime startTime, DateTime endTime)
        {
            var service = CreateService();
            await Assert.ThrowsAsync<ArgumentException>(() => service.GetRawDataListByCurrencyAsync(startTime, endTime, "CNY"));
        }

        [Theory]
        [MemberData(nameof(TestGenerator.GenerateCorrectTimeRange), MemberType = typeof(TestGenerator))]
        public async Task GetRawDataListByCurrency_WithGoodTimeRange_NotThrowException(DateTime startTime, DateTime endTime)
        {
            var stubApi = NSubstitute.Substitute.For<IApiClient>();
            var stubAppSetting = NSubstitute.Substitute.For<IAppSettings>();
            stubApi.GetResponseAsync(Arg.Compat.Any<string>(), Arg.Compat.Any<NameValueCollection>())
                .ReturnsForAnyArgs(Task.FromResult(this.SuccessReturn));
            stubAppSetting.GetValue(Arg.Compat.Any<string>()).Returns("");
            var service = CreateService(stubApi, stubAppSetting);

            await service.GetRawDataListByCurrencyAsync(startTime, endTime,"CNY");

            Assert.True(true);
        }

        [Theory]
        [MemberData(nameof(TestGenerator.GenerateResponseErrorCode), MemberType = typeof(TestGenerator))]
        public async Task GetXmlDataList_WhenCall_ThrowException(string errorReturn, DateTime startTime, DateTime endTime)
        {
            var stubApi = NSubstitute.Substitute.For<IApiClient>();
            var stubAppSetting = NSubstitute.Substitute.For<IAppSettings>();

            stubApi.GetResponseAsync(Arg.Compat.Any<string>(), Arg.Compat.Any<NameValueCollection>())
                .ReturnsForAnyArgs(errorReturn);
            stubAppSetting.GetValue(Arg.Compat.Any<string>()).Returns("");
            stubAppSetting.GetValue("VrLotteryCurrencies").Returns("CNC,BOB");

            var service = CreateService(stubApi, stubAppSetting);

            await Assert.ThrowsAsync<ThirdPartyProductException>(() => service.GetXmlDataListAsync(startTime,endTime));
        }

        [Fact]
        public async Task GetXmlDataList_WhenCall_NotThrowException()
        {
            var stubApi = NSubstitute.Substitute.For<IApiClient>();
            var stubAppSetting = NSubstitute.Substitute.For<IAppSettings>();
            var startTime = new DateTime(2019, 1, 1, 12, 0, 1);
            var endTime = new DateTime(2019, 1, 1, 13, 0, 0);

            stubApi.GetResponseAsync(Arg.Compat.Any<string>(), Arg.Compat.Any<NameValueCollection>())
                .ReturnsForAnyArgs(this.SuccessReturn);
            stubAppSetting.GetValue(Arg.Compat.Any<string>()).Returns("");
            stubAppSetting.GetValue("VrLotteryCurrencies").Returns("CNC,BOB");
            var service = CreateService(stubApi, stubAppSetting);

            await service.GetXmlDataListAsync(startTime, endTime);

            Assert.True(true);
        }
    }

}
