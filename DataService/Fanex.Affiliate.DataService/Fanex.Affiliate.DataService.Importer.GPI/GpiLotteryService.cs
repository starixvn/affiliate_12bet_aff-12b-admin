using Fanex.Affiliate.DataService.Importer.Core.Constants;
using Fanex.Affiliate.DataService.Importer.Core.Infrastructure;
using Fanex.Affiliate.DataService.Importer.Core.Repository;
using Fanex.Affiliate.DataService.Importer.Core.Utilities;
using Fanex.Affiliate.DataService.Importer.Core.Exceptions;
using Fanex.Affiliate.DataService.Importer.Core.Enum;
using Fanex.Affiliate.DataService.Importer.Gpi.Enum;
using Fanex.Affiliate.DataService.Importer.Gpi.Extension;
using Fanex.Affiliate.DataService.Importer.Gpi.MetaModels;
using Fanex.Affiliate.DataService.Importer.Gpi.Utilities;
using Fanex.Affiliate.DataService.Importer.Gpi.Client;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Fanex.Affiliate.DataService.Importer
{
    public class GpiLotteryService : XmlImpoterlServiceBase, IGetDataByGameType<GameType>
    {
        private readonly IApiClient _client;

        protected override int ProductId => Products.GPILotteryId;
        public GpiLotteryService(IApiClient client = null,IAppSettings appSetting = null, IThirdPartyProductRepository thirdPartyProdutRespository = null) 
            :base(appSetting, thirdPartyProdutRespository)
        {
            _client = client ?? new GpiLotteryClient();
        }

        public override async Task<IEnumerable<string>> GetRawDataListAsync(DateTime startTime, DateTime endTime)
        {
            EnsureEndTimeLaterThanStartTime(startTime, endTime);
            var gameTypesString = _appSetting.GetGpiLotteryGame();
            var datas = new List<string>();
            foreach (var typeInString in gameTypesString)
            {
                var type = GetGameType(typeInString);
                datas.AddRange(await GetRawDataByGameTypeAsync(startTime, endTime, type).ConfigureAwait(false));
            }
            return datas;
        }

        public virtual async Task<IEnumerable<string>> GetRawDataByGameTypeAsync(DateTime startTime, DateTime endTime, GameType type)
        {
            var pageNumber = 1;
            var paramForDateTime = new GpiParam
            {
                SDate = startTime,
                EDate = endTime,
                Game = type
            };
            var datas = new List<string>();
            while (true)
            {
                var rawData = await GetRawDataByPageAsync(paramForDateTime, pageNumber);
                datas.Add(rawData);
                pageNumber++;
                var isHasAnotherPage = await CheckHasAnotherPage(rawData);
                if (!isHasAnotherPage)
                    break;
            }
            return datas;
        }

        public override Task<XDocument> ConvertRawDataToXmlAsync(string raw)
        {
            var json = GpiHelper.BuildJsonString(raw);
            var xml = XmlHelper.ParseToXDocumentFromJson(json);
            if (xml == null)
                throw new ThirdPartyProductException($"GpiLottery - Cant convert response data to xml - {json}");
            return Task.FromResult(xml);
        }

        protected override void CheckXmlResponseData(XDocument xml)
        {
            if (xml.Root.IsEmpty)
                return;
            var elem = xml.Root.Element("transactions");
            if (elem == null)
                throw new ThirdPartyProductException($"GpiLotteryService - Not correct format {xml.ToString()}");
        }

        protected override bool CheckIsSuitableToImport(XDocument xml)
        {
            return !xml.Root.IsEmpty;
        }

        private async Task<string> GetRawDataByPageAsync(GpiParam param, int page = 1)
        {
            var paramForPage = BuildParam(param, page);
            var rawData = await GetRawDataAsync(paramForPage).ConfigureAwait(false);
            return rawData;
        }

        private async Task<bool> CheckHasAnotherPage(string json)
        {
            var pageSize = int.Parse(_appSetting.GetValue("GpiLotteryPageSize"));
            var xml = await ConvertRawDataToXmlAsync(json);
            CheckXmlResponseData(xml);
            var count = xml.Root.Elements("transactions").Count();
            return count == pageSize;
        }

        private async Task<string> GetRawDataAsync(GpiParam param)
        {
            var query = BuildApiQuery(param);
            return await GetDataStringAsync(query);
        }

        private async Task<string> GetDataStringAsync(NameValueCollection query)
        {
            var addressBase = _appSetting.GetValue("GpiLotterApiUrl");
            return await _client.GetResponseAsync(addressBase, query);
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
            var tranStatus = GetTransactionStatus();

            return new GpiParam
            {
                VendorName = vendorName,
                SDate = param.SDate,
                EDate = param.EDate,
                Game = param.Game,
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

        private static GameType GetGameType(string gameName)
        {
            GameType result;
            if (!System.Enum.TryParse(gameName, true, out result))
                throw new ArgumentException($"Wrong GpiLotteryGameType {gameName}");
            return result;
        }

    }
}
