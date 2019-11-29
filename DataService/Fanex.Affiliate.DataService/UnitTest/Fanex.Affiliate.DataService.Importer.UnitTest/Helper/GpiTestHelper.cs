using Fanex.Affiliate.DataService.Importer.Core.Infrastructure;
using Fanex.Affiliate.DataService.Importer.Core.Repository;
using Fanex.Affiliate.DataService.Importer.Core.Utilities;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fanex.Affiliate.DataService.Importer.UnitTest.Helper
{
    public static class GpiTestHelper
    {
        public static NameValueCollection GenerateConfig()
        {
            return new NameValueCollection
            {
                {"GpiLotterApiUrl", "https://lotterybo.gpiops.com/REST/ApiReport/GameTransactionDetail" },
                {"GpiLotteryVendorName", "PSI"},
                {"GpiLotteryPassword", "9CD1979E-CD3F-4B72-AC55-8D1A035AE13D" },
                {"GpiLotteryFormatDateTime", "yyyy-MM-dd HH:mm:ss"},
                {"GpiLotteryPageSize", "2" },
                {"GpiLotteryGameType", "ladder,rockpaperscissors"},
                {"GpiLotteryTransactionStatus", "Settled" }
            };
        }

        public static IAppSettings GetSetting(NameValueCollection config)
        {
            var stubSetting = NSubstitute.Substitute.For<IAppSettings>();
            foreach (var key in config.AllKeys)
            {
                stubSetting.GetValue(key).Returns(config[key]);
            }
            return stubSetting;
        }

        public static IApiClient GetApiClient(string value, params string[] values)
        {
            var client = NSubstitute.Substitute.For<IApiClient>();
            client.GetResponseAsync(Arg.Compat.Any<string>(), Arg.Compat.Any<NameValueCollection>())
                .Returns(value, values);
            return client;
        }

        public static IThirdPartyProductRepository GetRespository()
        {
            var repo = NSubstitute.Substitute.For<IThirdPartyProductRepository>();
            return repo;
        }

        public static string GenerateResponse(int numberOfTransaction)
        {
            var builder = new StringBuilder();
            builder.Append("[");
            for (int i = 0; i < numberOfTransaction; i++)
            {
                builder.Append("{code : \"1\"}");
                if (i < numberOfTransaction - 1)
                {
                    builder.Append(",");
                }
            }
            builder.Append("]");
            return builder.ToString();
        }
    }
}
