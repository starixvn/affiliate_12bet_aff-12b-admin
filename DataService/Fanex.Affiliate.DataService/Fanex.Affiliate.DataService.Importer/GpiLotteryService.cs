using Fanex.Affiliate.DataService.Importer.Constants;
using Fanex.Affiliate.DataService.Importer.Enum;
using Fanex.Affiliate.DataService.Importer.Enum.GPI;
using Fanex.Affiliate.DataService.Importer.MetaModels.GPI;
using Fanex.Affiliate.DataService.Importer.Repository;
using Fanex.Affiliate.DataService.Importer.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Fanex.Affiliate.DataService.Importer
{
    public class GpiLotteryService : IGpiLotteryService
    {
        private readonly IAppSettings _appSetting;
        private readonly IThirdPartyProductRepository _thirdPartyProductRepository;

        public GpiLotteryService(IAppSettings appSetting = null, IThirdPartyProductRepository thirdPartyProdutRespository = null)
        {
            _appSetting = appSetting ?? new AppSettings();
            _thirdPartyProductRepository = thirdPartyProdutRespository ?? new ThirdPartyProductRepository();
        }

        public void Import(DateTime startDate, DateTime endDate)
        {
            var rawDatas = GetXmlDataList(startDate, endDate);
            Import(rawDatas);
        }

        public void Import(List<string> xmls)
        {
            foreach (var xml in xmls)
            {
                this.Import(xml);
            }
        }

        public void Import(string xml)
        {
            _thirdPartyProductRepository.ImportData(Products.GPILottery, xml, 0);
        }

        public List<string> GetXmlDataList(DateTime startDate, DateTime endDate)
        {
            var rawDataList = new List<string>();
            if (DateTime.Compare(startDate, endDate) >=0)
            {
                throw new ArgumentException($"Start date: {startDate} is later or equal than end date: {endDate}");
            }
            var paramForFirstPage = BuildParam(new GpiParam()
            {
                SDate = startDate,
                EDate = endDate
            });

            var rawData = GetRawData(paramForFirstPage);
            var xmlData = XmlHelper.ParseToXDocumentFromJson(rawData, "root");

            rawDataList.Add(xmlData.ToString());

            return rawDataList;
        }

        public List<string>GetRawDataList(DateTime startDate, DateTime endDate)
        {
            var rawDataList = new List<string>();
            if (DateTime.Compare(startDate, endDate) >= 0)
            {
                throw new ArgumentException($"Start date: {startDate} is later or equal than end date: {endDate}");
            }
            var paramForFirstPage = BuildParam(new GpiParam()
            {
                SDate = startDate,
                EDate = endDate
            });

            var rawData = GetRawData(paramForFirstPage);
            rawDataList.Add(rawData);
            return rawDataList;
        }

        private string GetRawData(GpiParam param)
        {
            var query = BuildApiQuery(param);
            return GetDataString(query);
        }

        private string GetDataString(NameValueCollection query)
        {
            using (WebClient client = new WebClient())
            {
                var addressBase = _appSetting.GetValue("GpiLotterApiUrl");
                var responseByte = client.UploadValues(addressBase, "POST", query);
                var requestBody = Encoding.UTF8.GetString(responseByte);
                return requestBody;
            }
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

    }
}
