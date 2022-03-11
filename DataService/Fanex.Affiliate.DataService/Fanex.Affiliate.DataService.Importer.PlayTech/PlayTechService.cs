using Fanex.Affiliate.DataService.Importer.Core.Constants;
using Fanex.Affiliate.DataService.Importer.Core.DTO;
using Fanex.Affiliate.DataService.Importer.Core.Exceptions;
using Fanex.Affiliate.DataService.Importer.Core.Infrastructure;
using Fanex.Affiliate.DataService.Importer.Core.Repository;
using Fanex.Affiliate.DataService.Importer.Core.Utilities;
using Fanex.Affiliate.DataService.Importer.Playtech.MetaModels;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Fanex.Affiliate.DataService.Importer.Playtech
{
    public class PlaytechService : XmlImporterlServiceBase
    {
        protected readonly IApiClientFactory _apiClientFactory;
        protected override int ProductId => Products.PlaytechId;

        public PlaytechService(IApiClientFactory apiClientFactory = null, IAppSettings appSetting = null, IThirdPartyProductRepository thirdPartyProductRepository = null)
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

        private async Task<string> GetRawDataAsync(DateTimeOffset startTime, DateTimeOffset endTime)
        {
            var client = GetClient();
            var PlaytechParam = BuildPlaytechParam(startTime, endTime);

            var queryValues = BuildApiQuery(PlaytechParam);

            return await client.GetResponseAsync(queryValues).ConfigureAwait(false);
        }

        private IApiClient GetClient()
        {
            var url = $"{_appSetting.GetValue("PlaytechDailyDataURL")}";
            return _apiClientFactory.CreateApiClient(url, HttpMethod.Post);
        }

        private PlaytechParam BuildPlaytechParam(DateTimeOffset startTime, DateTimeOffset endTime)
        {
            var dateTimeFormat = _appSetting.GetValue("PlaytechFormatDate");

            return new PlaytechParam
            {
                ApiKey = _appSetting.GetValue("PlaytechApiKey"),
                Format = _appSetting.GetValue("PlaytechFormat"),
                Casino = _appSetting.GetValue("PlaytechCasino"),
                ReportId = _appSetting.GetValue("PlaytechReportId"),
                ReportBy = _appSetting.GetValue("PlaytechReportBy"),
                FromDate = startTime.ToString(dateTimeFormat),
                ToDate = endTime.ToString(dateTimeFormat)
            };
        }

        private NameValueCollection BuildApiQuery(PlaytechParam param)
        {
            return new NameValueCollection(7)
            {
                { "apikey", param.ApiKey },
                { "format", param.Format },
                { "startdate", param.FromDate },
                { "enddate", param.ToDate },
                { "casino", param.Casino },
                { "reportid", param.ReportId },
                { "reportBy", param.ReportBy }
            };
        }

        protected override void CheckXmlResponseData(XDocument xml)
        {
            if (xml.Root.IsEmpty)
                return;
            var elem = xml.Root.Element("trans");
            if (elem == null)
                throw new ThirdPartyProductException($"Playtech Product - Not correct format {xml}");
        }

        public override Task<XDocument> ConvertRawDataToXmlAsync(string raw)
        {
            return Task.FromResult(ConvertRawDataToXml(raw));
        }

        private XDocument ConvertRawDataToXml(string raw)
        {
            var xml = ConvertCSVToXml(raw);
            if (xml == null)
            {
                throw new ThirdPartyProductException($"Playtech Product - Can't convert json response to xml - {raw}");
            }
            return ToXDocument(xml);
        }

        private static XmlDocument ConvertCSVToXml(string rawData)
        {
            var doc = new XmlDocument();
            var root = doc.CreateElement("root");
            doc.AppendChild(root);

            // convert string to stream
            byte[] byteArray = Encoding.ASCII.GetBytes(rawData);
            using (MemoryStream stream = new MemoryStream(byteArray))
            {
                using (var streamReader = new StreamReader(stream))
                {
                    var hsColumnName = new Hashtable();
                    var lineContent = streamReader.ReadLine().Split(',');
                    for (int i = 0; i < lineContent.Length; i++)
                    {
                        if (!hsColumnName.ContainsKey(lineContent[i]))
                        {
                            hsColumnName.Add(lineContent[i], i);//Map order column to Column name
                        }
                    }

                    var indexDate = GetIndexOfColName(hsColumnName, ColumnName.Date);
                    var indexUsername = GetIndexOfColName(hsColumnName, ColumnName.Username);
                    var indexTurnover = GetIndexOfColName(hsColumnName, ColumnName.Turnover);
                    var indexDeposit = GetIndexOfColName(hsColumnName, ColumnName.Deposit);
                    var indexBonus = GetIndexOfColName(hsColumnName, ColumnName.Bonus);
                    var indexIAWinlost = GetIndexOfColName(hsColumnName, ColumnName.IAWinlost);
                    var indexWinlostAff = GetIndexOfColName(hsColumnName, ColumnName.Winlost);
                    var indexProgressiveShare = GetIndexOfColName(hsColumnName, ColumnName.ProgressiveShare);
                    var indexProgressiveWin = GetIndexOfColName(hsColumnName, ColumnName.ProgressiveWin);

                    while (!streamReader.EndOfStream)
                    {
                        lineContent = streamReader.ReadLine().Split(',');
                        if (!string.IsNullOrEmpty(lineContent[GetIndexOfColName(hsColumnName, "Username")]))
                        {
                            var transNode = doc.CreateElement("trans");
                            transNode.SetAttribute("date", lineContent[indexDate]);
                            transNode.SetAttribute("username", lineContent[indexUsername]);
                            transNode.SetAttribute("turnover", lineContent[indexTurnover]);
                            transNode.SetAttribute("deposit", lineContent[indexDeposit]);
                            transNode.SetAttribute("bonus", lineContent[indexBonus]);
                            transNode.SetAttribute("winlostaff", lineContent[indexWinlostAff]);
                            transNode.SetAttribute("winlost", lineContent[indexIAWinlost]);
                            transNode.SetAttribute("progressiveshare", lineContent[indexProgressiveShare]);
                            transNode.SetAttribute("progressivewin", lineContent[indexProgressiveWin]);
                            root.AppendChild(transNode);
                        }
                    }
                    return doc;
                }
            }
        }

        private static int GetIndexOfColName(Hashtable hs, string colName)
        {
            return Convert.ToInt32(hs[colName] ?? hs["\"" + colName + "\""]);
        }

        private static XDocument ToXDocument(XmlDocument xmlDocument)
        {
            using (var nodeReader = new XmlNodeReader(xmlDocument))
            {
                nodeReader.MoveToContent();
                return XDocument.Load(nodeReader);
            }
        }
    }
}
