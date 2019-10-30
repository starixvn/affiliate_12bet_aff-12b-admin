using Fanex.Affiliate.DataService.Importer.MetaModels.GPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;
using Xunit;
using Fanex.Affiliate.DataService.Importer.Utilities;

namespace Fanex.Affiliate.DataService.Importer.IntegratedTest
{
    public class WebClientTests
    {
        [Fact]
        public void WebClient_CallLocalGpiLotter_ReturnNotNull()
        {
            var url = "http://localhost:62166/api/home/GetGpiLotteryJson";
            using (var client = new WebClient())
            {
                NameValueCollection query = new NameValueCollection();
                query.Add("name1", "test1");
                query.Add("name2", "test2");

                var bytes = client.UploadValues(url, "POST", query);
                var rawData = Encoding.UTF8.GetString(bytes);

                Assert.NotEmpty(rawData);
            }
        }

        [Fact]
        public void ParseToXDocumentFromJson_ParseDateFromWebApi_ReturnNotNull()
        {
            var url = "http://localhost:62166/api/home/GetGpiLotteryJson";
            using (var client = new WebClient())
            {
                NameValueCollection query = new NameValueCollection();
                query.Add("name1", "test1");
                query.Add("name2", "test2");

                var bytes = client.UploadValues(url, "POST", query);
                var rawData = Encoding.UTF8.GetString(bytes);

                var xml = XmlHelper.ParseToXDocumentFromJson(rawData, "RESPONSE");
                var xmlString = xml.ToString();
                Assert.NotNull(xml);
            }
        }
    }
}
