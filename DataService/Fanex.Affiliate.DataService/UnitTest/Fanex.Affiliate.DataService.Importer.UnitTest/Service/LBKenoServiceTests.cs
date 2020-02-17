using Fanex.Affiliate.DataService.Importer.Core.Constants;
using Fanex.Affiliate.DataService.Importer.Core.Exceptions;
using Fanex.Affiliate.DataService.Importer.Core.Infrastructure;
using Fanex.Affiliate.DataService.Importer.Core.Repository;
using Fanex.Affiliate.DataService.Importer.Core.Utilities;
using Fanex.Affiliate.DataService.Importer.UnitTest.Helper;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Fanex.Affiliate.DataService.Importer.UnitTest
{
    public class LBKenoServiceTests
    {
        public LBKenoService CreateService(IApiClient client = null, IAppSettings setting = null, IThirdPartyProductRepository repo = null)
        {
            return new LBKenoService(client, setting, repo);
        }

        [Fact]
        public async Task ImportAsync_WhenCallWithSameDay_ThrowException()
        {
            var service = CreateService();
            var startTime = new DateTime(2019, 10, 10, 11, 0, 0);
            var endTime = new DateTime(2019, 10, 10, 14, 0, 0);

            await Assert.ThrowsAsync<ThirdPartyProductException>(() => service.ImportAsync(startTime, endTime));
        }

        [Fact]
        public async Task ConverRawDataToXmlAsync_WhenCallWithNotXmlFormat_ThrowException()
        {
            var raw = "{aaa : \"some strings are not format in xml\"}";
            var service = CreateService();

            await Assert.ThrowsAsync<ThirdPartyProductException>(() => service.ConvertRawDataToXmlAsync(raw));
        }

        [Fact]
        public async Task ConverRawDataToXmlAsync_WhenCallWithXmlFormat_NotThrowException()
        {
            var successResponse = "<game_transaction_detail>  <status_code>00</status_code> </game_transaction_detail>"; ;
            var service = CreateService();

            await service.ConvertRawDataToXmlAsync(successResponse);
            Assert.True(true);
        }

        [Fact]
        public async Task ImportAsync_WhenCallWithSuccessResponse_RepositoryCall()
        {
            var client = LBKenoTestHelper.GetSuccessResponseClient();
            var repo = LBKenoTestHelper.GetRepository();
            var setting = LBKenoTestHelper.GetAppSetting();
            var service = CreateService(client, setting, repo);
            var startTime = new DateTime(2019, 10, 9, 11, 0, 0);
            var endTime = new DateTime(2019, 10, 10, 14, 0, 0);

            await service.ImportAsync(startTime, endTime);

            await repo.Received().ImportDataAsync(Products.LBKenoId, Arg.Compat.Is<IEnumerable<string>>(datas => datas.Any()), 0);
        }

        [Fact]
        public async Task ImportAsync_WhenCallWithFailResponse_ThrowException()
        {
            var client = LBKenoTestHelper.GetFailResponseClient();
            var repo = LBKenoTestHelper.GetRepository();
            var setting = LBKenoTestHelper.GetAppSetting();
            var service = CreateService(client, setting, repo);
            var startTime = new DateTime(2019, 10, 9, 11, 0, 0);
            var endTime = new DateTime(2019, 10, 10, 14, 0, 0);

            await Assert.ThrowsAsync<ThirdPartyProductException>(() => service.ImportAsync(startTime, endTime));

        }

    }
}
