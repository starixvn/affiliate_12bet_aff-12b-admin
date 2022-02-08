using Fanex.Affiliate.DataService.Importer.Core.Infrastructure;
using Fanex.Affiliate.DataService.Importer.Core.Repository;
using Fanex.Affiliate.DataService.Importer.Core.Utilities;
using Newtonsoft.Json.Linq;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fanex.Affiliate.DataService.Importer.UnitTest.Helper
{
    public class TopazTestHelper
    {
        public static string RESPONSE_SUCCESS => @"{'status':'SUCCESS', 'access_token': 'test'}";

        public static string RESPONSE_EXCESS_LIMIT_CALL => @"{'status':'FAIL','message':'CALL_LIMIT_EXCEEDED','errors':[], 'access_token': 'test'}";

        public static string RESPONSE_INVALID_VALUE => @"{'status':'FAIL','message':null,'errors':[{'field':'brandId','error':'INVALID_VALUE'}], 'access_token': 'test'}";

        public static string RESPONSE_BRAND_NOT_CONFIG => @"{'status':'FAIL','message':null,'errors':[{'field':'brandId','error':'NOT_ENABLED'}], 'access_token': 'test'}";

        public static NameValueCollection GenerateConfig()
        {
            return new NameValueCollection
            {
                {"TopazApiUrl",  "https://datafeed.adminfg.com/ps/ssw/datafeed/v2/transactionsTimeRange"},
                {"TopazBrandId", "171" },
                {"TopazBrandPassword", "E9nkT38V1!zWjj" },
                {"TopazFormatDateTime", "yyyy-MM-ddThh:mm:ss" },
            };
        }
        public static IAppSettings GetStubAppSetting()
        {
            var stubSetting = Substitute.For<IAppSettings>();
            var config = GenerateConfig();
            foreach (var key in config.AllKeys)
            {
                stubSetting.GetValue(key).Returns(config[key]);
            }
            return stubSetting;
        }

        public static IApiClientFactory GetApiClient()
        {
            var stubClient = Substitute.For<IApiClientFactory>();
            return stubClient;
        }

        public static IThirdPartyProductRepository GetStubRepo()
        {
            var stubRepo = Substitute.For<IThirdPartyProductRepository>();
            return stubRepo;
        }
    }
}
