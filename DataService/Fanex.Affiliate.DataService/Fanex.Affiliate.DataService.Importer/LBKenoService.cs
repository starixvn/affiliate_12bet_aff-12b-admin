using Fanex.Affiliate.DataService.Importer.Constants;
using Fanex.Affiliate.DataService.Importer.Enum;
using Fanex.Affiliate.DataService.Importer.Exceptions;
using Fanex.Affiliate.DataService.Importer.MetaModels.LBKeno;
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
    public class LBKenoService : ImporterXmlServiceBase
    {
        private const string OK_STATUS = "00";
        private const string HOST_ACCESS_DENIED_STATUS = "40.01";

        public LBKenoService(IAppSettings appSetting, IThirdPartyProductRepository thirdPartyProductRepository) 
            : base(appSetting, thirdPartyProductRepository)
        {
        }

        public override sealed void Import(string data)
        {
            _thirdPartyProductRepository.ImportData(Products.LBKeno, data, 0);
        }

        protected override sealed string ConvertToXmlString(string data)
        {
            return data;
        }

        public override sealed IEnumerable<string> GetRawDataList(DateTime startTime, DateTime endTime)
        {
            var rawDatas = new List<string>();
            var paramToRequest = BuildParam(startTime, endTime);

            var data = GetRawData(paramToRequest);
            rawDatas.Add(data);
            return rawDatas;
        }

        private string GetRawData(LBKenoParam paramToRequest)
        {
            var queryValues = BuildQueryParam(paramToRequest);
            string data = "";
            using(var client = new WebClient())
            {
                var url = _appSetting.GetValue("LBKenoApiUrl");
                var bytes = client.UploadValues(url, "POST", queryValues);
                data = Encoding.UTF8.GetString(bytes);
            }
            CheckData(data);
            return data;

        }

        private void CheckData(string data)
        {
            var xml = XmlHelper.ParseToXDocument(data);
            if (xml == null)
                throw new ThirdPartyProductException($"Error: LBKeno - xDocument is null\n{data}");
            var statusCode = xml.Root.Element("status_code").Value.Trim();
            if (statusCode != OK_STATUS)
            {
                if (statusCode == HOST_ACCESS_DENIED_STATUS)
                    throw new ThirdPartyProductException($"Error: LBKeno - {HOST_ACCESS_DENIED_STATUS} Invalid secret key and/or IP address not white listed");
                else
                    throw new ThirdPartyProductException($"Error: LBKeno - {data}");
            }
        }

        private NameValueCollection BuildQueryParam(LBKenoParam paramToRequest)
        {
            var dateTimeFormat = _appSetting.GetValue("LBKenoFormatDateTime");
            var queryValues = new NameValueCollection(7);
            queryValues.Add("secret_key", paramToRequest.SecretKey);
            queryValues.Add("operator_id", paramToRequest.OperatorId);
            queryValues.Add("site_code", paramToRequest.SiteCode);
            queryValues.Add("product_code", paramToRequest.ProductCode);
            queryValues.Add("start_time", paramToRequest.StartTime.ToString(dateTimeFormat));
            queryValues.Add("end_time", paramToRequest.EndTime.ToString(dateTimeFormat));
            queryValues.Add("status", paramToRequest.Status.ToString().ToLower());
            return queryValues;
        }

        private LBKenoParam BuildParam(DateTime startTime, DateTime endTime)
        {
            return new LBKenoParam()
            {
                SecretKey = _appSetting.GetValue("LBKenoSecretKey"),
                OperatorId = _appSetting.GetValue("LBKenoOperatorId"),
                ProductCode = _appSetting.GetValue("LBKenoProductCode"),
                SiteCode = _appSetting.GetValue("LBKenoSiteCode"),
                StartTime = startTime,
                EndTime = endTime,
                Status = GetTransactionStatus(),
            };
        }

        private TransactionStatus GetTransactionStatus()
        {
            TransactionStatus result;
            var transactionName = _appSetting.GetValue("LBKenoTransactionStatus");
            if (!System.Enum.TryParse(transactionName, true, out result))
                throw new ArgumentException($"Wrong GpiTransactionStatus {transactionName}");
            return result;
        }
    }
}
