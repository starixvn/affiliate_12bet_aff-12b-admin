using Fanex.Affiliate.DataService.Importer.Client;
using Fanex.Affiliate.DataService.Importer.Constants;
using Fanex.Affiliate.DataService.Importer.Core.Infrastructure;
using Fanex.Affiliate.DataService.Importer.Enum;
using Fanex.Affiliate.DataService.Importer.Enum.GPI;
using Fanex.Affiliate.DataService.Importer.Exceptions;
using Fanex.Affiliate.DataService.Importer.MetaModels.GPI;
using Fanex.Affiliate.DataService.Importer.Repository;
using Fanex.Affiliate.DataService.Importer.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Fanex.Affiliate.DataService.Importer
{
    public class GpiLotteryService : XmlImpoterlServiceBase
    {
        private readonly IApiClient _client;
        private bool disposed = false;
        public GpiLotteryService(IApiClient client = null,IAppSettings appSetting = null, IThirdPartyProductRepository thirdPartyProdutRespository = null) 
            :base(appSetting, thirdPartyProdutRespository)
        {
            _client = client ?? new GpiLotteryClient();
        }

        public override void Import(string data)
        {
            _thirdPartyProductRepository.ImportData(Products.GPILotteryId, data, 0);
        }

        public override IEnumerable<string>GetRawDataList(DateTime startTime, DateTime endTime)
        {
            EnsureEndTimeLaterThanStartTime(startTime, endTime);
            
            var pageNumber = 1;
            var paramForDateTime = new GpiParam()
            {
                SDate = startTime,
                EDate = endTime
            };

            while(true)
            {
                var rawData = GetRawDataByPage(paramForDateTime, pageNumber);
                yield return rawData;
                pageNumber++;
                if (!HasAnotherPage(rawData))
                    break;
            }
        }

        public override XDocument ConvertRawDataToXml(string raw)
        {
            var json = BuildJsonString(raw);
            var xml = XmlHelper.ParseToXDocumentFromJson(json);
            if (xml == null)
                throw new ThirdPartyProductException($"GpiLottery - Cant convert response data to xml - {json}");
            return xml;
        }

        private string BuildJsonString(string data)
        {
            var builder = new StringBuilder();
            builder.Append("{")
                .Append("transactions")
                .Append(":")
                .Append(data)
                .Append("}");
            return builder.ToString();
        }

        protected override void CheckXmlResponseData(XDocument xml)
        {
            if (xml.Root.IsEmpty)
                return;
            var elem = xml.Root.Element("transactions");
            if (elem == null)
                throw new ThirdPartyProductException($"GpiLotteryService - Not correct format {xml.ToString()}");;
        }

        protected override bool CheckIsSuitableToImport(XDocument xml)
        {
            return xml.Root.IsEmpty;
        }

        private string GetRawDataByPage(GpiParam param, int page = 1)
        {
            var paramForFirstPage = BuildParam(param, page);
            var rawData = GetRawData(paramForFirstPage);
            return rawData;
        }

        private bool HasAnotherPage(string json)
        {
            var pageSize = int.Parse(_appSetting.GetValue("GpiLotteryPageSize"));
            var xml = ConvertRawDataToXml(json);
            CheckXmlResponseData(xml);
            var count = xml.Root.Elements("transactions").Count();
            return count == pageSize;
        }

        private string GetRawData(GpiParam param)
        {
            var query = BuildApiQuery(param);
            return GetDataString(query);
        }

        private string GetDataString(NameValueCollection query)
        {
            var addressBase = _appSetting.GetValue("GpiLotterApiUrl");
            return _client.GetResponse(addressBase, query);
        }

        private NameValueCollection BuildApiQuery(GpiParam param)
        {
            var queryParam = new NameValueCollection(9);
            var dateTimeFormat = _appSetting.GetValue("GpiLotteryFormatDateTime");
            queryParam.Add("vendorName", param.VendorName);
            queryParam.Add("game", GpiHelper.GetGameTypeName(param.Game));
            queryParam.Add("sDate", param.SDate.ToString(dateTimeFormat));
            queryParam.Add("eDate", param.EDate.ToString(dateTimeFormat));
            queryParam.Add("status", GpiHelper.GetTransactionStatusName(param.TransactionStatus));
            queryParam.Add("password", param.Password);
            queryParam.Add("pageNumber", param.PageNumber.ToString());
            queryParam.Add("pageSize", param.PageSize.ToString());
            return queryParam;
        }

        private GpiParam BuildParam(GpiParam param, int pageNumber = 1)
        {
            var vendorName = _appSetting.GetValue("GpiLotteryVendorName");
            var password = _appSetting.GetValue("GpiLotteryPassword");
            var pageSize = int.Parse(_appSetting.GetValue("GpiLotteryPageSize"));
            var gameType = GetGameType();
            var tranStatus = GetTransactionStatus();

            return new GpiParam()
            {
                VendorName = vendorName,
                SDate = param.SDate,
                EDate = param.EDate,
                Game = gameType,
                TransactionStatus = tranStatus,
                Password = password,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        private TransactionStatus  GetTransactionStatus()
        {
            TransactionStatus result;
            var transactionName = _appSetting.GetValue("GpiLotteryTransactionStatus");
            if (!System.Enum.TryParse(transactionName, true, out result))
                throw new ArgumentException($"Wrong GpiTransactionStatus {transactionName}");
            return result;
        }

        private GameType GetGameType()
        {
            GameType result;
            var gameName = _appSetting.GetValue("GpiLotteryGameType");
            if (!System.Enum.TryParse(gameName, true, out result))
                throw new ArgumentException($"Wrong GpiLotteryGameType {gameName}");
            return result;
        }

        public override void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                _client?.Dispose();
            }

            disposed = true;
        }

        ~GpiLotteryService()
        {
            Dispose(false);
        }
    }
}
