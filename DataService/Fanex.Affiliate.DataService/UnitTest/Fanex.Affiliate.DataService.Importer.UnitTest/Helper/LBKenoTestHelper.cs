using Fanex.Affiliate.DataService.Importer.Core.Infrastructure;
using Fanex.Affiliate.DataService.Importer.Core.Repository;
using Fanex.Affiliate.DataService.Importer.Core.Utilities;
using NSubstitute;
using System.Collections.Specialized;
using System.Threading;

namespace Fanex.Affiliate.DataService.Importer.UnitTest.Helper
{
    public static class LBKenoTestHelper
    {
        public static NameValueCollection GenerateConfig()
        {
            return new NameValueCollection
            {
                {"LBKenoApiUrl", "http://12bet.kenoapi.lb-keno-game.net/keno_game_transaction_detail.ashx" },
                {"LBKenoSecretKey", "56AD30CB34" },
                {"LBKenoOperatorId", "364317BC-DF0A-ABA8-F059-4F21A619460D"},
                {"LBKenoFormatDateTime", "yyyy-MM-dd" },
                {"LBKenoSiteCode", "12bet" },
                {"LBKenoProductCode", "keno" },
                {"LBKenoTransactionStatus", "Settled" },
            };
        }

        public static IAppSettings GetAppSetting()
        {
            var setting = Substitute.For<IAppSettings>();
            var config = GenerateConfig();
            foreach (var key in config.AllKeys)
            {
                setting.GetValue(key).Returns(config[key]);
            }
            return setting;
        }

        public static IApiClientFactory GetSuccessResponseClient()
        {
            var successResponse = @"<game_transaction_detail>
                                      <status_code>00</status_code>
                                      <status_text>OK</status_text>
                                      <bet>
                                        <item  bet_time=""2019-11-06 22:47:05"" trans_time=""2019-11-06 22:47:00""/>
                                      </bet >
                                   </game_transaction_detail>";
            var client = Substitute.For<IApiClientFactory>();
            client.CreateApiClient(Arg.Compat.Any<string>()).GetResponseAsync(Arg.Compat.Any<NameValueCollection>(), Arg.Compat.Any<CancellationToken>())
                .Returns(successResponse);
            return client;

        }

        public static IApiClientFactory GetFailResponseClient()
        {
            var successResponse = "<game_transaction_detail>  <status_code>40.01</status_code> </game_transaction_detail>";
            var client = Substitute.For<IApiClientFactory>();
            client.CreateApiClient(Arg.Compat.Any<string>()).GetResponseAsync(Arg.Compat.Any<NameValueCollection>(), Arg.Compat.Any<CancellationToken>())
                .Returns(successResponse);
            return client;
        }

        public static IThirdPartyProductRepository GetRepository()
        {
            return Substitute.For<IThirdPartyProductRepository>();
        }
    }
}
