using Fanex.Affiliate.DataService.Importer.Core.Constants;
using Fanex.Affiliate.DataService.Importer.Core.Infrastructure;
using Fanex.Affiliate.DataService.Importer.Core.Repository;
using Fanex.Affiliate.DataService.Importer.Core.ResponseChecker;
using Fanex.Affiliate.DataService.Importer.Core.Utilities;
using Fanex.Affiliate.DataService.Importer.Jasper.Helpers;
using Fanex.Affiliate.DataService.Importer.Jasper.MetaModels;
using Fanex.Affiliate.DataService.Importer.Jasper.ResponseChecker;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml.Linq;

namespace Fanex.Affiliate.DataService.Importer.Jasper
{
    public class JasperService : XmlImporterlServiceBase
    {
        protected override int ProductId => Products.JasperId;

        protected readonly IApiClientFactory _apiClientFactory;

        public JasperService(IApiClientFactory apiClientFactory = null, IAppSettings appSetting = null, IThirdPartyProductRepository thirdPartyProductRepository = null)
            : base(appSetting, thirdPartyProductRepository)
        {
            _apiClientFactory = apiClientFactory ?? new ApiClientFactory();
        }

        public override async Task<IEnumerable<string>> GetRawDataListAsync(DateTime startTime, DateTime endTime)
        {
            var datas = new List<string>
            {
                await GetRawDataAsync(startTime, endTime).ConfigureAwait(false)
            };

            return datas;
        }

        private async Task<string> GetXmlAsync()
        {
            using (var stream = new StreamReader("../../App_Data/JasperData/Jasper.xml"))
            {
                return await stream.ReadToEndAsync();
            }
        }

        public override Task<XDocument> ConvertRawDataToXmlAsync(string raw)
        {
            var dataXml = XmlHelper.ParseToXDocument(raw);

            return Task.FromResult(ReFormatXml(dataXml));
        }

        private XDocument ReFormatXml(XDocument xml)
        {
            var root = xml.Root;
            var status = root.Element("status");

            if (Validations.IsRequestSuccess(status))
            {
                var games = root.Element("result").Element("gameinfo").Elements("game");
                if(games.Count() != 0)
                {
                    var newGames = new List<XElement>();

                    foreach (var game in games)
                    {
                        var deals = GetDealXmls(game);

                        game.ReplaceNodes(deals);

                        newGames.Add(game);
                    }

                    root.Element("result").Element("gameinfo").ReplaceNodes(newGames);
                }
            }

            return xml;
        }

        private IEnumerable<XElement> GetDealXmls(XElement gameXml)
        {
            var deals = gameXml.Elements("deal");

            var newDeals = new List<XElement>();

            if (deals.Count() != 0)
            {
                foreach(var deal in deals)
                {
                    var newDeal = new XElement("deal");
                    newDeal.SetElementValue("dealid", deal.Attribute("id").Value);
                    SetElementToXElement(newDeal, deal, GetDealFieldNames());

                    var clientbet = deal.Element("betinfo").Element("clientbet");
                    SetElementToXElement(newDeal, clientbet, GetClientBetFieldNames());

                    newDeals.Add(newDeal);
                }
            }

            return newDeals;
        }

        private static void SetElementToXElement(XElement newXElement, XElement oldXElement, IEnumerable<string> fieldNames)
        {
            foreach (var fieldName in fieldNames)
            {
                newXElement.SetElementValue(fieldName, oldXElement.Attribute(fieldName).Value);
            }
        }

        private async Task<string> GetRawDataAsync(DateTimeOffset startTime, DateTimeOffset endTime)
        {
            var jasperParam = BuildJasperParam(startTime, endTime);
            var requestXml = BuildApiRequestXml(jasperParam);
            var client = GetClient(requestXml);

            return await client.GetResponseAsync(new NameValueCollection()).ConfigureAwait(false);
        }

        private IApiClient GetClient(string requestXml)
        {
            //var header = new HttpClientHeaderOption
            //{
            //    AcceptContent = "*/*",
            //    Headers = new List<NameValueHeaderValue> {
            //            new NameValueHeaderValue("Accept-Encoding", "gzip")
            //        }
            //};

            var url = $"{_appSetting.GetValue("JasperApiUrl")}";
            var client = _apiClientFactory.CreateApiClient(url, HttpMethod.Post);
            client.ContentProvider = new XmlContentProvider(HttpUtility.HtmlEncode(requestXml));

            return client;
        }

        protected override void CheckXmlResponseData(XDocument xml)
        {
            IThirdPartyResponseChecker checker = new JasperResponseChecker();
            checker.CheckXml(xml);
        }

        private string BuildApiRequestXml(JasperParam param)
        {
            return "<?xml version=\"1.0\" encoding=\"utf-16\"?>"
                + $"<request action=\"${param.Action}\">"
                + "<element>"
                + $"<properties name=\"vendorid\">${param.VendorId}</properties>"
                + $"<properties name=\"merchantpasscode\">${param.MerchantPassCode}</properties>"
                + $"<properties name=\"timezone\">${param.TimeZone}</properties>"
                + $"<properties name=\"startdate\">${param.StartDate}</properties>"
                + $"<properties name=\"enddate\">${param.EndDate}</properties>"
                + "</element>"
                + "</request>";
        }

        private JasperParam BuildJasperParam(DateTimeOffset startTime, DateTimeOffset endTime)
        {
            var dateTimeFormat = _appSetting.GetValue("JasperFormatDate");

            return new JasperParam
            {
                Action = _appSetting.GetValue("JasperAction"),
                VendorId = _appSetting.GetValue("JasperVendorId"),
                MerchantPassCode = _appSetting.GetValue("JasperMerchantPassCode"),
                TimeZone = _appSetting.GetValue("JasperTimeZone"),
                StartDate = startTime.ToString(dateTimeFormat),
                EndDate = endTime.ToString(dateTimeFormat)
            };
        }

        private static string[] GetDealFieldNames()
            => new[]
            {
                "startdate", "enddate", "status", "code"
            };

        private static string[] GetClientBetFieldNames()
            => new[]
            {
                "betid", "login", "valid_turnover", "hold",
            };
    }
}
