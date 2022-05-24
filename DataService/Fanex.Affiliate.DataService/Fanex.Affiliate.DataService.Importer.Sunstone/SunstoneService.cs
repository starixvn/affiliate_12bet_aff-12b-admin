using Fanex.Affiliate.DataService.Importer.Core.Constants;
using Fanex.Affiliate.DataService.Importer.Core.Infrastructure;
using Fanex.Affiliate.DataService.Importer.Core.Repository;
using Fanex.Affiliate.DataService.Importer.Core.ResponseChecker;
using Fanex.Affiliate.DataService.Importer.Core.Utilities;
using Fanex.Affiliate.DataService.Importer.Jasper.Helpers;
using Fanex.Affiliate.DataService.Importer.Sunstone.MetaModels;
using Fanex.Affiliate.DataService.Importer.Sunstone.ResponseChecker;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Fanex.Affiliate.DataService.Importer.Sunstone
{
    public class SunstoneService : XmlImporterlServiceBase
    {
        protected override int ProductId => Products.SunstoneId;

        protected readonly IApiClientFactory _apiClientFactory;

        public SunstoneService(IApiClientFactory apiClientFactory = null, IAppSettings appSetting = null, IThirdPartyProductRepository thirdPartyProductRepository = null)
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

        public override Task<XDocument> ConvertRawDataToXmlAsync(string raw)
        {
            var dataXml = XmlHelper.ParseToXDocument(raw);

            return Task.FromResult(ReFormatXml(dataXml));
        }

        private XDocument ReFormatXml(XDocument xml)
        {
            var element = xml.Root.Element("element");
            var status = element.Element("status");

            if (Validations.IsRequestSuccess(status))
            {
                var games = element.Element("result").Elements("game");
                if(games.Any())
                {
                    var newGames = new List<XElement>();

                    foreach (var game in games)
                    {
                        var deals = GetDealXmls(game);

                        game.ReplaceNodes(deals);

                        newGames.Add(game);
                    }

                    element.Element("result").ReplaceNodes(newGames);
                }
            }

            return xml;
        }

        private IEnumerable<XElement> GetDealXmls(XElement gameXml)
        {
            var deals = gameXml.Elements("deal");

            var newDealList = new List<XElement>();

            if (deals.Any())
            {
                foreach(var deal in deals)
                {
                    var newDeal = new XElement("deal");
                    newDeal.SetElementValue("dealid", deal.Attribute("id").Value);
                    SetElementToXElement(newDeal, deal, GetDealFieldNames());

                    var clientBets = deal.Element("betinfo").Elements("clientbet");
                    var newClientBetList = new List<XElement>();

                    foreach (var clientBet in clientBets)
                    {
                        var newClientBet = new XElement("clientbet");
                        SetElementToXElement(newClientBet, clientBet, GetClientBetFieldNames());

                        newClientBetList.Add(newClientBet);
                    }

                    newDeal.Add(newClientBetList);
                    newDealList.Add(newDeal);
                }
            }

            return newDealList;
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
            var SunstoneParam = BuildSunstoneParam(startTime, endTime);
            var requestXml = BuildApiRequestXml(SunstoneParam);
            var client = GetClient(requestXml);

            return await client.GetResponseAsync(new NameValueCollection()).ConfigureAwait(false);
        }

        private IApiClient GetClient(string requestXml)
        {
            var url = $"{_appSetting.GetValue("SunstoneApiUrl")}";
            var client = _apiClientFactory.CreateApiClient(url, HttpMethod.Post);
            client.ContentProvider = new XmlContentProvider(requestXml);

            return client;
        }

        protected override void CheckXmlResponseData(XDocument xml)
        {
            IThirdPartyResponseChecker checker = new SunstoneResponseChecker();
            checker.CheckXml(xml);
        }

        private string BuildApiRequestXml(SunstoneParam param)
        {
            return $"<request action=\"{param.Action}\">"
                + $"<element id=\"{param.ElementId}\">"
                + $"<properties name=\"vendorid\">{param.VendorId}</properties>"
                + $"<properties name=\"timezone\">{param.TimeZone}</properties>"
                + $"<properties name=\"startdate\">{param.StartDate}</properties>"
                + $"<properties name=\"enddate\">{param.EndDate}</properties>"
                + "</element>"
                + "</request>";
        }

        private SunstoneParam BuildSunstoneParam(DateTimeOffset startTime, DateTimeOffset endTime)
        {
            var dateTimeFormat = _appSetting.GetValue("SunstoneFormatDate");

            return new SunstoneParam
            {
                Action = _appSetting.GetValue("SunstoneAction"),
                VendorId = _appSetting.GetValue("SunstoneVendorId"),
                ElementId = _appSetting.GetValue("SunstoneElementId"),
                TimeZone = _appSetting.GetValue("SunstoneTimeZone"),
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
