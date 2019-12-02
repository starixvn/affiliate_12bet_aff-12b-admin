using Fanex.Affiliate.DataService.Importer.UnitTest.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fanex.Affiliate.DataService.Importer.Core.Infrastructure;
using Fanex.Affiliate.DataService.Importer.Core.Utilities;
using Fanex.Affiliate.DataService.Importer.Core.Repository;
using NSubstitute;
using System.Collections.Specialized;
using Xunit;
using Fanex.Affiliate.DataService.Importer.Core.Exceptions;
using Fanex.Affiliate.DataService.Importer.Core.Constants;

namespace Fanex.Affiliate.DataService.Importer.UnitTest.Service
{
    public class TopazServiceTest
    {
        private TopazService CreateService(IApiClient stubClient, IAppSettings stubSetting, IThirdPartyProductRepository repo = null)
        {
            var service = new TopazService(stubClient, stubSetting, repo);
            service.IsTestMode = true;
            return service;
        }

        [Fact]
        public async Task GetRawDataListAsync_WhenCallWithSuccessResponse_ReturnTrue()
        {
            var stubClient = TopazTestHelper.GetApiClient();
            var stubSetting = TopazTestHelper.GetStubAppSetting();
            stubClient.GetResponseAsync(default(string), default(NameValueCollection))
                .ReturnsForAnyArgs(TopazTestHelper.RESPONSE_SUCCESS);

            var service = CreateService(stubClient, stubSetting);
            var startTime = new DateTime(2019, 10, 10, 10, 0, 0);
            var endTime = startTime.AddMinutes(15);

            await service.GetRawDataListAsync(startTime, endTime);

            Assert.True(true);
        }

        [Fact]
        public async Task GetRawDataListAsync_WhenCallWithFirstResponseIsExcessLimit_ClientHasCallTwoTime()
        {
            var stubClient = TopazTestHelper.GetApiClient();
            var stubSetting = TopazTestHelper.GetStubAppSetting();
            stubClient.GetResponseAsync(default(string), default(NameValueCollection))
                .ReturnsForAnyArgs(TopazTestHelper.RESPONSE_EXCESS_LIMIT_CALL, TopazTestHelper.RESPONSE_SUCCESS);

            var service = CreateService(stubClient, stubSetting);
            var startTime = new DateTime(2019, 10, 10, 10, 0, 0);
            var endTime = startTime.AddMinutes(15);

            await service.GetRawDataListAsync(startTime, endTime);

            await stubClient.ReceivedWithAnyArgs(2).GetResponseAsync(default(string), default(NameValueCollection));
        }

        [Fact]
        public async Task GetRawDataListAsync_WhenCallWithThirtyMinuteTimeRange_ClientHasCallTwoTimes()
        {
            var stubClient = TopazTestHelper.GetApiClient();
            var stubSetting = TopazTestHelper.GetStubAppSetting();
            stubClient.GetResponseAsync(default(string), default(NameValueCollection))
                .ReturnsForAnyArgs(TopazTestHelper.RESPONSE_SUCCESS);

            var service = CreateService(stubClient, stubSetting);
            var startTime = new DateTime(2019, 10, 10, 10, 0, 0);
            var endTime = startTime.AddMinutes(30);

            await service.GetRawDataListAsync(startTime, endTime);

            await stubClient.ReceivedWithAnyArgs(2).GetResponseAsync(default(string), default(NameValueCollection));
        }

        [Fact]
        public async Task GetRawDataListAsync_WhenCallWithFirstResponseIsExcessLimit_ReturnListHasOneMember()
        {
            var stubClient = TopazTestHelper.GetApiClient();
            var stubSetting = TopazTestHelper.GetStubAppSetting();
            stubClient.GetResponseAsync(default(string), default(NameValueCollection))
                .ReturnsForAnyArgs(TopazTestHelper.RESPONSE_EXCESS_LIMIT_CALL, TopazTestHelper.RESPONSE_SUCCESS);

            var service = CreateService(stubClient, stubSetting);
            var startTime = new DateTime(2019, 10, 10, 10, 0, 0);
            var endTime = startTime.AddMinutes(15);

            var list = await service.GetRawDataListAsync(startTime, endTime);

            Assert.Single(list);
        }

        [Fact]
        public async Task GetRawDataListAsync_WhenCallWithFirstResponseIsExcessLimitAndTimeRangeIsThirtyMinute_ReturnListHasTwoMember()
        {
            var stubClient = TopazTestHelper.GetApiClient();
            var stubSetting = TopazTestHelper.GetStubAppSetting();
            stubClient.GetResponseAsync(default(string), default(NameValueCollection))
                .ReturnsForAnyArgs(TopazTestHelper.RESPONSE_EXCESS_LIMIT_CALL
                , TopazTestHelper.RESPONSE_SUCCESS
                , TopazTestHelper.RESPONSE_EXCESS_LIMIT_CALL
                , TopazTestHelper.RESPONSE_SUCCESS);

            var service = CreateService(stubClient, stubSetting);
            var startTime = new DateTime(2019, 10, 10, 10, 0, 0);
            var endTime = startTime.AddMinutes(30);

            var list = await service.GetRawDataListAsync(startTime, endTime);

            Assert.Equal(2,list.Count());
        }

        [Fact]
        public async Task ImportAsync_WhenCallWithResponseFail_ThrowException()
        {
            var stubClient = TopazTestHelper.GetApiClient();
            var stubSetting = TopazTestHelper.GetStubAppSetting();
            var stubRepo = TopazTestHelper.GetStubRepo();
            stubClient.GetResponseAsync(default(string), default(NameValueCollection))
                .ReturnsForAnyArgs(TopazTestHelper.RESPONSE_INVALID_VALUE);

            var service = CreateService(stubClient, stubSetting, stubRepo);
            var startTime = new DateTime(2019, 10, 10, 10, 0, 0);
            var endTime = startTime.AddMinutes(15);

            await Assert.ThrowsAnyAsync<ThirdPartyProductException>(() => service.ImportAsync(startTime, endTime));
        }

        [Fact]
        public async Task ImportAsync_WhenCallWithFirstResponseIsExcessLimit_RepoCalled()
        {
            var stubClient = TopazTestHelper.GetApiClient();
            var stubSetting = TopazTestHelper.GetStubAppSetting();
            var stubRepo = TopazTestHelper.GetStubRepo();
            stubClient.GetResponseAsync(default(string), default(NameValueCollection))
                .ReturnsForAnyArgs(TopazTestHelper.RESPONSE_EXCESS_LIMIT_CALL, TopazTestHelper.RESPONSE_SUCCESS);

            var service = CreateService(stubClient, stubSetting, stubRepo);
            var startTime = new DateTime(2019, 10, 10, 10, 0, 0);
            var endTime = startTime.AddMinutes(15);

            await service.ImportAsync(startTime, endTime);

            await stubRepo.Received(1).ImportDataAsync(Products.TopazId, Arg.Compat.Any<IEnumerable<string>>(), 0);
        }

        [Fact]
        public async Task ImportAsync_WhenCallWithTwoTimeExccessLimitConsecutive_ThrowException()
        {
            var stubClient = TopazTestHelper.GetApiClient();
            var stubSetting = TopazTestHelper.GetStubAppSetting();
            var stubRepo = TopazTestHelper.GetStubRepo();
            stubClient.GetResponseAsync(default(string), default(NameValueCollection))
                .ReturnsForAnyArgs(TopazTestHelper.RESPONSE_EXCESS_LIMIT_CALL, TopazTestHelper.RESPONSE_EXCESS_LIMIT_CALL);

            var service = CreateService(stubClient, stubSetting, stubRepo);
            var startTime = new DateTime(2019, 10, 10, 10, 0, 0);
            var endTime = startTime.AddMinutes(15);

            await Assert.ThrowsAnyAsync<ThirdPartyProductException>(() => service.ImportAsync(startTime, endTime));
        }
    }
}
