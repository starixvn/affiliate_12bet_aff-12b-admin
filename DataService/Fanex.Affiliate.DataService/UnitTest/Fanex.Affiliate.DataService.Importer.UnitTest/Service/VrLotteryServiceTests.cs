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
using Fanex.Affiliate.DataService.Importer.UnitTest.Helper;
using System.Linq;
using Fanex.Affiliate.DataService.Importer.Core.Constants;
using System.Threading;
using System.Net.Http;

namespace Fanex.Affiliate.DataService.Importer.IMOneTests
{
    public class VrLotteryServiceTests
    {
        private VrLotteryService CreateService(IApiClientFactory client = null, IAppSettings appSetting = null, IThirdPartyProductRepository repo = null)
        {
            return new VrLotteryService(client, appSetting, repo);
        }

        private int SetUpResponse(IApiClientFactory stubApi, IEnumerable<Tuple<DateTime, DateTime>> ranges, int[] pagesForEachRequest)
        {
            var lstResponse = new List<string>();
            for (int i = 0; i < ranges.Count(); i++)
            {
                int numberOfPage = 1;
                if (pagesForEachRequest.Count() > i)
                    numberOfPage = pagesForEachRequest[i];
                lstResponse.AddRange(VrLotteryTestHelper.GenerateSuccessResponse(numberOfPage));
            }
            stubApi.CreateApiClient(Arg.Compat.Any<string>(), Arg.Compat.Any<HttpMethod>(), Arg.Compat.Any<HttpClientHeaderOption>()).GetResponseAsync(default(NameValueCollection), default(CancellationToken))
                .ReturnsForAnyArgs(lstResponse.First(), lstResponse.Skip(1).ToArray());
            return lstResponse.Count;
        }

        [Fact]
        public async Task GetRawDataListByCurrency_WithNotCorrectTimeRange_ThrowException()
        {
            var startTime = new DateTime(2019, 10, 10, 10, 0, 0);
            var endTime = startTime;
            var stubClientFactory = VrLotteryTestHelper.GetStubApi();
            var service = CreateService(stubClientFactory);
            await Assert.ThrowsAsync<ArgumentException>(() => service.GetRawDataListByCurrencyAsync(startTime, endTime, "CNY"));
        }

        [Fact]
        public async Task GetRawDataListByCurrency_WithGoodTimeRange_NotThrowException()
        {
            var startTime = new DateTime(2019, 10, 10, 10, 0, 0);
            var endTime = startTime.Add(new TimeSpan(0, 10, 0));
            var stubApi = VrLotteryTestHelper.GetStubApi();
            var stubAppSetting = VrLotteryTestHelper.GetStubSetting();
            stubApi.CreateApiClient(Arg.Compat.Any<string>(), Arg.Compat.Any<HttpMethod>(), Arg.Compat.Any<HttpClientHeaderOption>()).GetResponseAsync(Arg.Compat.Any<NameValueCollection>(), Arg.Compat.Any<CancellationToken>())
                .ReturnsForAnyArgs(VrLotteryTestHelper.GenerateResponse(IMOne.IMoneResponseStatus.SUCCESS));
            var service = CreateService(stubApi, stubAppSetting);

            await service.GetRawDataListByCurrencyAsync(startTime, endTime,"CNY");

            Assert.True(true);
        }

        [Fact]
        public async Task GetXmlDataList_WhenCall_ThrowException()
        {
            var startTime = new DateTime(2019, 10, 10, 10, 0, 0);
            var endTime = startTime.Add(new TimeSpan(0, 10, 0));
            var stubApi = VrLotteryTestHelper.GetStubApi();
            var stubAppSetting = VrLotteryTestHelper.GetStubSetting();
            stubApi.CreateApiClient(Arg.Compat.Any<string>(), Arg.Compat.Any<HttpMethod>(), Arg.Compat.Any<HttpClientHeaderOption>()).GetResponseAsync(Arg.Compat.Any<NameValueCollection>(), Arg.Compat.Any<CancellationToken>())
                .ReturnsForAnyArgs(VrLotteryTestHelper.GenerateResponse(IMOne.IMoneResponseStatus.INVALID_CODEWALLET));

            var service = CreateService(stubApi, stubAppSetting);

            await Assert.ThrowsAsync<ThirdPartyProductException>(() => service.GetXmlDataListAsync(startTime,endTime));
        }

        [Fact]
        public async Task GetXmlDataList_WhenCall_NotThrowException()
        {
            var startTime = new DateTime(2019, 10, 10, 10, 0, 0);
            var endTime = startTime.Add(new TimeSpan(0, 10, 0));
            var stubApi = VrLotteryTestHelper.GetStubApi();
            var stubAppSetting = VrLotteryTestHelper.GetStubSetting();

            stubApi.CreateApiClient(Arg.Compat.Any<string>(), Arg.Compat.Any<HttpMethod>(), Arg.Compat.Any<HttpClientHeaderOption>()).GetResponseAsync(Arg.Compat.Any<NameValueCollection>(), Arg.Compat.Any<CancellationToken>())
                .ReturnsForAnyArgs(VrLotteryTestHelper.GenerateResponse(IMOne.IMoneResponseStatus.SUCCESS));
            var service = CreateService(stubApi, stubAppSetting);

            await service.GetXmlDataListAsync(startTime, endTime);

            Assert.True(true);
        }

        [Theory]
        [InlineData(40, 1,2,2,1)]
        [InlineData(15, 2, 1)]
        public async Task GetRawDataListByCurrency_WhenCallWithSuccessResponse_CallToApiClient(int minute, params int[] pagesForEachRequest)
        {
            var startTime = new DateTime(2019, 10, 10, 10, 0, 0);
            var endTime = startTime.Add(new TimeSpan(0,minute,0));
            var stubApi = VrLotteryTestHelper.GetStubApi();
            var stubSetting = VrLotteryTestHelper.GetStubSetting();
            var ranges = DateTimeHelper.GetProportionTimeRange(startTime, endTime, new TimeSpan(0, 10, 0));
            int numberOfReponse = SetUpResponse(stubApi, ranges, pagesForEachRequest);

            var service = CreateService(stubApi, stubSetting);


            await service.GetRawDataListByCurrencyAsync(startTime, endTime, "CNY");

            await stubApi.CreateApiClient(Arg.Compat.Any<string>(), Arg.Compat.Any<HttpMethod>(), Arg.Compat.Any<HttpClientHeaderOption>()).ReceivedWithAnyArgs(numberOfReponse).GetResponseAsync(default(NameValueCollection), default(CancellationToken));
        }

        [Fact]
        public async Task GetRawDataListByCurrency_WhenCallWithFailResponse_NotThrowException()
        {
            var startTime = new DateTime(2019, 10, 10, 10, 0, 0);
            var endTime = startTime.Add(new TimeSpan(0, 10, 0));
            var stubApi = VrLotteryTestHelper.GetStubApi();
            var stubSetting = VrLotteryTestHelper.GetStubSetting();
            stubApi.CreateApiClient(Arg.Compat.Any<string>(), Arg.Compat.Any<HttpMethod>(), Arg.Compat.Any<HttpClientHeaderOption>()).GetResponseAsync(default(NameValueCollection), default(CancellationToken))
                .ReturnsForAnyArgs(VrLotteryTestHelper.GenerateResponse(IMOne.IMoneResponseStatus.INVALID_CODEWALLET));
            var service = CreateService(stubApi, stubSetting);

            await service.GetRawDataListByCurrencyAsync(startTime, endTime, "CNY");

            Assert.True(true);
        }

        [Fact]
        public async Task GetRawDataListByCurrency_WhenCall_ApiClientReceiveCorrectParam()
        {
            var config = VrLotteryTestHelper.GenerateConfig();
            var currency = "CNY";
            var startTime = new DateTime(2019, 10, 10, 10, 0, 0);
            var endTime = startTime.Add(new TimeSpan(0, 10, 0));

            var stubSetting = VrLotteryTestHelper.GetStubSetting();
            var url = stubSetting.GetValue("VrLotteryApiUrl");
            var stubApi = VrLotteryTestHelper.GetStubApi();
            stubApi.CreateApiClient(url, Arg.Compat.Any<HttpMethod>(), Arg.Compat.Any<HttpClientHeaderOption>()).GetResponseAsync(default(NameValueCollection), default(CancellationToken))
                .ReturnsForAnyArgs(VrLotteryTestHelper.GenerateResponse(IMOne.IMoneResponseStatus.SUCCESS));

            var service = CreateService(stubApi, stubSetting);
            var query = new NameValueCollection
            {
                { "MerchantCode", stubSetting.GetValue("VrLotteryMerchantCode") },
                { "StartDate", startTime.ToString(IMOne.Constants.IMOneDateTimeFormat) },
                { "EndDate", endTime.ToString(IMOne.Constants.IMOneDateTimeFormat) },
                { "ProductWallet", stubSetting.GetValue("VrLotteryProductWallet") },
                { "PageSize", VrLotteryService.MAX_PAGE_SIZE.ToString() },
                { "Currency", currency },
                { "Page", "1" }
            };

            await service.GetRawDataListByCurrencyAsync(startTime, endTime, currency);

            stubApi.Received().CreateApiClient(url, Arg.Compat.Any<HttpMethod>(), Arg.Compat.Any<HttpClientHeaderOption>());
            await stubApi.CreateApiClient(url, Arg.Compat.Any<HttpMethod>()).Received().GetResponseAsync(Arg.Compat.Is<NameValueCollection>(q => q.HasAllValue(query)), default(CancellationToken));
        }

        [Fact]
        public async Task GetRawDataListByCurrecies_WhenCall_ApiClientReponseMultipleTime()
        {
            var startTime = new DateTime(2019, 10, 10, 10, 0, 0);
            var endTime = startTime.Add(new TimeSpan(0, 10, 0));
            var stubApi = VrLotteryTestHelper.GetStubApi();
            var stubSetting = VrLotteryTestHelper.GetStubSetting();
            stubApi.CreateApiClient(Arg.Compat.Any<string>(), Arg.Compat.Any<HttpMethod>(), Arg.Compat.Any<HttpClientHeaderOption>()).GetResponseAsync(default(NameValueCollection), default(CancellationToken))
                .ReturnsForAnyArgs(VrLotteryTestHelper.GenerateResponse(IMOne.IMoneResponseStatus.SUCCESS));
            var service = CreateService(stubApi, stubSetting);
            var currencies = new List<string> { "CNY", "ABC" };

            await service.GetRawDataListByCurrenciesAsync(startTime, endTime, currencies);

            await stubApi.CreateApiClient(Arg.Compat.Any<string>(), Arg.Compat.Any<HttpMethod>(), Arg.Compat.Any<HttpClientHeaderOption>()).ReceivedWithAnyArgs(2).GetResponseAsync(default(NameValueCollection), default(CancellationToken));
        }

        [Theory]
        [InlineData(40, 1, 2, 2, 1)]
        [InlineData(15, 2, 1)]
        public async Task ImportAsync_WhenCall_CallToThirdPartyRepo(int minute, params int[] pagesForEachRequest)
        {
            var startTime = new DateTime(2019, 10, 10, 10, 0, 0);
            var endTime = startTime.Add(new TimeSpan(0, minute, 0));
            var stubApi = VrLotteryTestHelper.GetStubApi();
            var stubSetting = VrLotteryTestHelper.GetStubSetting();
            var stubRepo = VrLotteryTestHelper.GetStubRepo();
            var ranges = DateTimeHelper.GetProportionTimeRange(startTime, endTime, new TimeSpan(0, 10, 0));
            int numberOfReponse = SetUpResponse(stubApi, ranges, pagesForEachRequest);

            var service = CreateService(stubApi, stubSetting, stubRepo);

            await service.ImportAsync(startTime, endTime);

            await stubRepo.Received().ImportDataAsync(Products.VRLotteryId, Arg.Compat.Is<IEnumerable<string>>(datas => datas.Count() == numberOfReponse), 0);
        }

        [Fact]
        public async Task ImportAsync_WhenCallWithNotDataFoundResponse_ThirdPartyRepoReceiveNoData()
        {
            var startTime = new DateTime(2019, 10, 10, 10, 0, 0);
            var endTime = startTime.AddMinutes(10);
            var stubApi = VrLotteryTestHelper.GetStubApi();
            var stubSetting = VrLotteryTestHelper.GetStubSetting();
            var stubRepo = VrLotteryTestHelper.GetStubRepo();
            stubApi.CreateApiClient(Arg.Compat.Any<string>(), Arg.Compat.Any<HttpMethod>(), Arg.Compat.Any<HttpClientHeaderOption>()).GetResponseAsync(default(NameValueCollection), default(CancellationToken))
                .ReturnsForAnyArgs(VrLotteryTestHelper.GenerateResponse(IMOne.IMoneResponseStatus.NO_DATA_FOUND));
            var service = CreateService(stubApi, stubSetting, stubRepo);

            await  service.ImportAsync(startTime, endTime);

            await stubRepo.Received().ImportDataAsync(Products.VRLotteryId, Arg.Compat.Is<IEnumerable<string>>(a => !a.Any()), 0);
        }

        [Fact]
        public async Task ImportAsync_WhenCallWithFailResponse_ThrowException()
        {
            var startTime = new DateTime(2019, 10, 10, 10, 0, 0);
            var endTime = startTime.AddMinutes(10);
            var stubApi = VrLotteryTestHelper.GetStubApi();
            var stubSetting = VrLotteryTestHelper.GetStubSetting();
            var stubRepo = VrLotteryTestHelper.GetStubRepo();
            stubApi.CreateApiClient(Arg.Compat.Any<string>(), Arg.Compat.Any<HttpMethod>(), Arg.Compat.Any<HttpClientHeaderOption>()).GetResponseAsync(default(NameValueCollection), default(CancellationToken))
                .ReturnsForAnyArgs(VrLotteryTestHelper.GenerateResponse(IMOne.IMoneResponseStatus.INVALID_CODEWALLET));
            var service = CreateService(stubApi, stubSetting, stubRepo);

            await Assert.ThrowsAsync<ThirdPartyProductException>(() => service.ImportAsync(startTime,endTime));
        }
    }

}
