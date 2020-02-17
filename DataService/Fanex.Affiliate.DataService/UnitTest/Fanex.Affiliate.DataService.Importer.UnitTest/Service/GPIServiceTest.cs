using Fanex.Affiliate.DataService.Importer.Core.Constants;
using Fanex.Affiliate.DataService.Importer.Core.Exceptions;
using Fanex.Affiliate.DataService.Importer.Core.Infrastructure;
using Fanex.Affiliate.DataService.Importer.Core.Repository;
using Fanex.Affiliate.DataService.Importer.Core.Utilities;
using Fanex.Affiliate.DataService.Importer.Gpi.Enum;
using Fanex.Affiliate.DataService.Importer.Gpi.Extension;
using Fanex.Affiliate.DataService.Importer.Gpi.Utilities;
using Fanex.Affiliate.DataService.Importer.UnitTest.Helper;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using Xunit;


namespace Fanex.Affiliate.DataService.Importer.UnitTest
{

    public class GPIServiceTests
    {
        private GpiLotteryService CreateGpiLotteryService(IApiClient client = null, IAppSettings setting = null, IThirdPartyProductRepository repo = null)
        {
            return new GpiLotteryService(client, setting, repo);
        }


        [Theory]
        [InlineData(GameType.Keno, "keno")]
        [InlineData(GameType.Fast3, "fast3")]
        [InlineData(GameType.Pk10, "pk10")]
        public void CheckEnumName_GameType_ReturnTrue(GameType type, string expected)
        {
            Assert.Contains(expected, GpiHelper.GetGameTypeName(type));
        }

        [Fact]
        public async Task ConvertRawDataToXmlAsync_WhenCallWithNotCorrectFormat_ThrowException()
        {
            var service = CreateGpiLotteryService();
            var response = "some input not array json";
            await Assert.ThrowsAsync<ThirdPartyProductException>(() =>  service.ConvertRawDataToXmlAsync(response));
        }

        [Fact]
        public async Task ConvertRawDataToXmlAsync_WhenCallWithCorrectJsonFormat_NotThrowException()
        {
            var service = CreateGpiLotteryService();
            var response = GpiTestHelper.GenerateResponse(0);
            await service.ConvertRawDataToXmlAsync(response);
            Assert.True(true);
        }

        [Fact]
        public async Task GetRawDataByGameTypeAsync_WhenCall_CallApiClient()
        {
            var config = GpiTestHelper.GenerateConfig();
            var stubClient = GpiTestHelper.GetApiClient(GpiTestHelper.GenerateResponse(0));
            var stubSetting = GpiTestHelper.GetSetting(config);
            var stubRepo = GpiTestHelper.GetRespository();
            var service = CreateGpiLotteryService(stubClient, stubSetting, stubRepo);
            var startTime = new DateTime(2019, 10, 10, 11, 0, 0);
            var endTime = new DateTime(2019, 10, 10, 12, 0, 0);

            await service.GetRawDataByGameTypeAsync(startTime, endTime, GameType.RockPaperScissors);

            await stubClient
                .Received()
                .GetResponseAsync(config["GpiLotterApiUrl"], Arg.Compat
                    .Is<NameValueCollection>(v => v["game"].Contains(GpiHelper.GetGameTypeName(GameType.RockPaperScissors))));
        }

        [Fact]
        public async Task GetRawDataByGameTypeAsync_WhenCall_CallApiClientMultipleTime()
        {
            var config = GpiTestHelper.GenerateConfig();
            //set up so that number transaction in json equal to config page size (default is 2 generate by GenerateConfg())
            var stubClient = GpiTestHelper.GetApiClient(GpiTestHelper.GenerateResponse(2),GpiTestHelper.GenerateResponse(0));
            var stubSetting = GpiTestHelper.GetSetting(config);
            var stubRepo = GpiTestHelper.GetRespository();
            var service = CreateGpiLotteryService(stubClient, stubSetting, stubRepo);
            var startTime = new DateTime(2019, 10, 10, 11, 0, 0);
            var endTime = new DateTime(2019, 10, 10, 12, 0, 0);

            await service.GetRawDataByGameTypeAsync(startTime, endTime, GameType.RockPaperScissors);

            await stubClient.ReceivedWithAnyArgs(2).GetResponseAsync(default(string) , default(NameValueCollection));
        }

        [Fact]
        public async Task GetRawDalaListAsync_WhenCall_CallApiMultiple()
        {
            var config = GpiTestHelper.GenerateConfig();
            config["GpiLotteryGameType"] = "pk10,ladder";
            //set up so that number transaction in json equal to config page size (default is 2 generate by GenerateConfg())
            var stubClient = GpiTestHelper.GetApiClient(GpiTestHelper.GenerateResponse(2), GpiTestHelper.GenerateResponse(1));
            var stubSetting = GpiTestHelper.GetSetting(config);
            var stubRepo = GpiTestHelper.GetRespository();
            var service = CreateGpiLotteryService(stubClient, stubSetting, stubRepo);
            var startTime = new DateTime(2019, 10, 10, 11, 0, 0);
            var endTime = new DateTime(2019, 10, 10, 12, 0, 0);
            var numberOfGame = stubSetting.GetGpiLotteryGame().Count();
            var totalCall = numberOfGame + 1;

            await service.GetRawDataListAsync(startTime, endTime);

            await stubClient.ReceivedWithAnyArgs(totalCall).GetResponseAsync(default(string), default(NameValueCollection));
        }

        [Fact]
        public async Task ImportAsync_WhenCall_CallThirdPartyMultiple()
        {
            var config = GpiTestHelper.GenerateConfig();
            //set up so that number transaction in json equal to config page size (default is 2 generate by GenerateConfg())
            var stubClient = GpiTestHelper.GetApiClient(GpiTestHelper.GenerateResponse(2), GpiTestHelper.GenerateResponse(1));
            var stubSetting = GpiTestHelper.GetSetting(config);
            var stubRepo = GpiTestHelper.GetRespository();
            var service = CreateGpiLotteryService(stubClient, stubSetting, stubRepo);
            var startTime = new DateTime(2019, 10, 10, 11, 0, 0);
            var endTime = new DateTime(2019, 10, 10, 12, 0, 0);
            var numberOfGame = stubSetting.GetGpiLotteryGame().Count();
            var totalCall = numberOfGame + 1;

            await service.ImportAsync(startTime, endTime);

            await stubRepo.Received()
                .ImportDataAsync(Products.GPILotteryId, Arg.Compat.Is<IEnumerable<string>>(datas => datas.Count() == totalCall), 0);
        }

        [Fact]
        public async Task ImportAsync_WhenCall_DidNotCallThirdPartyWithEmptyArrayResponse()
        {
            var config = GpiTestHelper.GenerateConfig();
            var stubClient = GpiTestHelper.GetApiClient(GpiTestHelper.GenerateResponse(0));
            var stubSetting = GpiTestHelper.GetSetting(config);
            var stubRepo = GpiTestHelper.GetRespository();
            var service = CreateGpiLotteryService(stubClient, stubSetting, stubRepo);
            var startTime = new DateTime(2019, 10, 10, 11, 0, 0);
            var endTime = new DateTime(2019, 10, 10, 12, 0, 0);

            await service.ImportAsync(startTime, endTime);

            await stubRepo.Received().ImportDataAsync(Arg.Compat.Any<int>(), Arg.Compat.Is<IEnumerable<string>>(datas => !datas.Any()), Arg.Compat.Any<byte>());
        }
    }
}
