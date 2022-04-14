using Fanex.Affiliate.DataService.Importer.UnitTest.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fanex.Affiliate.DataService.Importer.Core.Infrastructure;
using Fanex.Affiliate.DataService.Importer.Core.Utilities;
using Fanex.Affiliate.DataService.Importer.Core.Repository;
using NSubstitute;
using System.Collections.Specialized;
using Xunit;
using Fanex.Affiliate.DataService.Importer.Core.Exceptions;
using Fanex.Affiliate.DataService.Importer.Core.Constants;
using System.Threading;
using System.Net.Http;

namespace Fanex.Affiliate.DataService.Importer.UnitTest.Service
{
    public class TopazServiceTest
    {
        private TopazService CreateService(IApiClientFactory stubClient, IAppSettings stubSetting, IThirdPartyProductRepository repo = null)
        {
            var service = new TopazService(stubClient, stubSetting, repo);
            return service;
        }

        [Fact]
        public async Task GetRawDataListAsync_WhenCallWithSuccessResponse_ReturnTrue()
        {
            var stubClient = TopazTestHelper.GetApiClient();
            var stubSetting = TopazTestHelper.GetStubAppSetting();
            stubClient.CreateApiClient(Arg.Compat.Any<string>(), Arg.Compat.Any<HttpMethod>(), Arg.Compat.Any<HttpClientHeaderOption>()).GetResponseAsync(default(NameValueCollection), default(CancellationToken))
                .ReturnsForAnyArgs(TopazTestHelper.RESPONSE_SUCCESS);

            var service = CreateService(stubClient, stubSetting);
            var startTime = new DateTime(2019, 10, 10, 10, 0, 0);
            var endTime = startTime.AddMinutes(60);

            await service.GetRawDataListAsync(startTime, endTime);

            Assert.True(true);
        }

        [Fact]
        public async Task GetRawDataListAsync_WhenCallWithFirstResponseIsExcessLimit_ClientHasCallTwoTime()
        {
            var stubClient = TopazTestHelper.GetApiClient();
            var stubSetting = TopazTestHelper.GetStubAppSetting();
            stubClient.CreateApiClient(Arg.Compat.Any<string>(), Arg.Compat.Any<HttpMethod>(), Arg.Compat.Any<HttpClientHeaderOption>()).GetResponseAsync(default(NameValueCollection), default(CancellationToken))
                .ReturnsForAnyArgs(TopazTestHelper.RESPONSE_EXCESS_LIMIT_CALL, TopazTestHelper.RESPONSE_SUCCESS);

            var service = CreateService(stubClient, stubSetting);
            var startTime = new DateTime(2019, 10, 10, 10, 0, 0);
            var endTime = startTime.AddMinutes(60);

            await service.GetRawDataListAsync(startTime, endTime);

            await stubClient.CreateApiClient(Arg.Compat.Any<string>(), Arg.Compat.Any<HttpMethod>(), Arg.Compat.Any<HttpClientHeaderOption>()).ReceivedWithAnyArgs(2).GetResponseAsync(default(NameValueCollection), default(CancellationToken));
        }

        [Fact]
        public async Task GetRawDataListAsync_WhenCallWithThirtyMinuteTimeRange_ClientHasCallTwoTimes()
        {
            var stubClient = TopazTestHelper.GetApiClient();
            var stubSetting = TopazTestHelper.GetStubAppSetting();
            stubClient.CreateApiClient(Arg.Compat.Any<string>(), Arg.Compat.Any<HttpMethod>(), Arg.Compat.Any<HttpClientHeaderOption>()).GetResponseAsync(default(NameValueCollection), default(CancellationToken))
                .ReturnsForAnyArgs(TopazTestHelper.RESPONSE_SUCCESS);

            var service = CreateService(stubClient, stubSetting);
            var startTime = new DateTime(2019, 10, 10, 10, 0, 0);
            var endTime = startTime.AddMinutes(120);

            await service.GetRawDataListAsync(startTime, endTime);

            stubClient.CreateApiClient(Arg.Compat.Any<string>(), Arg.Compat.Any<HttpMethod>(), Arg.Compat.Any<HttpClientHeaderOption>()).ReceivedWithAnyArgs(2);
        }

        [Fact]
        public async Task GetRawDataListAsync_WhenCallWithFirstResponseIsExcessLimit_ReturnListHasOneMember()
        {
            var stubClient = TopazTestHelper.GetApiClient();
            var stubSetting = TopazTestHelper.GetStubAppSetting();
            stubClient.CreateApiClient(Arg.Compat.Any<string>(), Arg.Compat.Any<HttpMethod>(), Arg.Compat.Any<HttpClientHeaderOption>()).GetResponseAsync(default(NameValueCollection), default(CancellationToken))
                .ReturnsForAnyArgs(TopazTestHelper.RESPONSE_EXCESS_LIMIT_CALL, TopazTestHelper.RESPONSE_SUCCESS);

            var service = CreateService(stubClient, stubSetting);
            var startTime = new DateTime(2019, 10, 10, 10, 0, 0);
            var endTime = startTime.AddMinutes(60);

            var list = await service.GetRawDataListAsync(startTime, endTime);

            Assert.Single(list);
        }

        [Fact]
        public async Task GetRawDataListAsync_WhenCallWithFirstResponseIsExcessLimitAndTimeRangeIsThirtyMinute_ReturnListHasTwoMember()
        {
            var stubClient = TopazTestHelper.GetApiClient();
            var stubSetting = TopazTestHelper.GetStubAppSetting();
            stubClient.CreateApiClient(Arg.Compat.Any<string>(), Arg.Compat.Any<HttpMethod>(), Arg.Compat.Any<HttpClientHeaderOption>()).GetResponseAsync(default(NameValueCollection), default(CancellationToken))
                .ReturnsForAnyArgs(TopazTestHelper.RESPONSE_EXCESS_LIMIT_CALL
                , TopazTestHelper.RESPONSE_SUCCESS
                , TopazTestHelper.RESPONSE_EXCESS_LIMIT_CALL
                , TopazTestHelper.RESPONSE_SUCCESS);

            var service = CreateService(stubClient, stubSetting);
            var startTime = new DateTime(2019, 10, 10, 10, 0, 0);
            var endTime = startTime.AddMinutes(120);

            var list = await service.GetRawDataListAsync(startTime, endTime);

            Assert.Equal(2,list.Count());
        }

        [Fact]
        public async Task ImportAsync_WhenCallWithResponseFail_ThrowException()
        {
            var stubClient = TopazTestHelper.GetApiClient();
            var stubSetting = TopazTestHelper.GetStubAppSetting();
            var stubRepo = TopazTestHelper.GetStubRepo();
            stubClient.CreateApiClient(Arg.Compat.Any<string>(), Arg.Compat.Any<HttpMethod>(), Arg.Compat.Any<HttpClientHeaderOption>()).GetResponseAsync(default(NameValueCollection), default(CancellationToken))
                .ReturnsForAnyArgs(TopazTestHelper.RESPONSE_INVALID_VALUE, string.Empty);

            var service = CreateService(stubClient, stubSetting, stubRepo);
            var startTime = new DateTime(2019, 10, 10, 10, 0, 0);
            var endTime = startTime.AddMinutes(60);

            await Assert.ThrowsAnyAsync<ThirdPartyProductException>(() => service.ImportAsync(startTime, endTime));
        }

        [Fact]
        public async Task ImportAsync_WhenCallWithFirstResponseIsExcessLimit_RepoCalled()
        {
            var stubClient = TopazTestHelper.GetApiClient();
            var stubSetting = TopazTestHelper.GetStubAppSetting();
            var stubRepo = TopazTestHelper.GetStubRepo();
            stubClient.CreateApiClient(Arg.Compat.Any<string>(), Arg.Compat.Any<HttpMethod>(), Arg.Compat.Any<HttpClientHeaderOption>()).GetResponseAsync(default(NameValueCollection), default(CancellationToken))
                .ReturnsForAnyArgs(TopazTestHelper.RESPONSE_EXCESS_LIMIT_CALL, TopazTestHelper.RESPONSE_SUCCESS);

            var service = CreateService(stubClient, stubSetting, stubRepo);
            var startTime = new DateTime(2019, 10, 10, 10, 0, 0);
            var endTime = startTime.AddMinutes(60);

            await service.ImportAsync(startTime, endTime);

            await stubRepo.Received(1).ImportDataAsync(Products.TopazId, Arg.Compat.Any<IEnumerable<string>>(), 0);
        }

        [Fact]
        public async Task ImportAsync_WhenCallWithTwoTimeExccessLimitConsecutive_ThrowException()
        {
            var stubClient = TopazTestHelper.GetApiClient();
            var stubSetting = TopazTestHelper.GetStubAppSetting();
            var stubRepo = TopazTestHelper.GetStubRepo();
            stubClient.CreateApiClient(Arg.Compat.Any<string>(), Arg.Compat.Any<HttpMethod>(), Arg.Compat.Any<HttpClientHeaderOption>()).GetResponseAsync(default(NameValueCollection), default(CancellationToken))
                .ReturnsForAnyArgs(TopazTestHelper.RESPONSE_EXCESS_LIMIT_CALL, string.Empty);

            var service = CreateService(stubClient, stubSetting, stubRepo);
            var startTime = new DateTime(2019, 10, 10, 10, 0, 0);
            var endTime = startTime.AddMinutes(60);

            await Assert.ThrowsAnyAsync<ThirdPartyProductException>(() => service.ImportAsync(startTime, endTime));
        }
    }
}
