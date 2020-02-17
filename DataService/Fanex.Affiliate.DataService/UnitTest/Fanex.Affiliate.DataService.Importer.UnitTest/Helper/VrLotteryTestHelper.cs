using Fanex.Affiliate.DataService.Importer.Core.Infrastructure;
using Fanex.Affiliate.DataService.Importer.Core.Repository;
using Fanex.Affiliate.DataService.Importer.Core.Utilities;
using Fanex.Affiliate.DataService.Importer.IMOne;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fanex.Affiliate.DataService.Importer.UnitTest.Helper
{
    public static class VrLotteryTestHelper
    {
        public static NameValueCollection GenerateConfig()
        {
            var config = new NameValueCollection
            {
                {"VrLotteryApiUrl", "http://imone.imaegisapi.com/Report/GetBetLog" },
                {"VrLotteryMerchantCode", "OkkMzjaRW9tac3HsBejgKKcchnTkbcMW"},
                {"VrLotteryCurrencies", "CNY" },
                {"VrLotteryProductWallet", "503" },
            };
            return config;
        }

        public static IEnumerable<string> GenerateSuccessResponse(int page = 1)
        {
            for (int i = 1; i <= page; i++)
            {
                var builder = new StringBuilder();
                builder.Append("{")
                    .AppendLine("\"Code\":\"0\",")
                    .AppendLine("\"Pagination\":").Append("{")
                    .AppendLine($"\"CurrentPage\":{i},")
                    .AppendLine($"\"TotalPage\":{page}").Append("}")
                    .AppendLine("}");
                yield return builder.ToString();
            }
        }

        public static string GenerateBetDetailInProcessResponse()
        {
            var builder = new StringBuilder();
            builder.Append("{")
                    .AppendLine("\"Code\":\"527\",")
                    .AppendLine("}");
            return builder.ToString();
        }

        public static string GenerateResponse(IMoneResponseStatus status)
        {
            if (status == IMoneResponseStatus.SUCCESS)
                return GenerateSuccessResponse(1).First();

            var builder = new StringBuilder();
            builder.Append("{")
                    .AppendLine($"\"Code\":\"{(int)status}\",")
                    .AppendLine("}");
            return builder.ToString();
        }

        public static IApiClient GetStubApi()
        {
            var client = NSubstitute.Substitute.For<IApiClient>();
            return client;
        }

        public static IAppSettings GetStubSetting()
        {
            var setting = NSubstitute.Substitute.For<IAppSettings>();
            var config = GenerateConfig();
            foreach (var key in config.AllKeys)
            {
                setting.GetValue(key).Returns(config[key]);
            }
            return setting;
        }

        public static IThirdPartyProductRepository GetStubRepo()
        {
            var repo = NSubstitute.Substitute.For<IThirdPartyProductRepository>();
            return repo;
        }

        public static bool HasAllValue(this NameValueCollection collect1, NameValueCollection collect2)
        {
            var result = true;
            try
            {
                foreach (var key in collect2.AllKeys)
                {
                    result = result && collect1[key].Equals(collect2[key]);
                }
            }
            catch (Exception)
            {

                result = false;
            }
            return result;
        }
    }
}
