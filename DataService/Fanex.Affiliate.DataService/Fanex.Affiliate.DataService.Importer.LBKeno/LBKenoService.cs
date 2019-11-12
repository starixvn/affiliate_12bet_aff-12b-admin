using Fanex.Affiliate.DataService.Importer.Constants;
using Fanex.Affiliate.DataService.Importer.Core.Infrastructure;
using Fanex.Affiliate.DataService.Importer.Enum;
using Fanex.Affiliate.DataService.Importer.Exceptions;
using Fanex.Affiliate.DataService.Importer.LBKeno;
using Fanex.Affiliate.DataService.Importer.LBKeno.Client;
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
using System.Xml.Linq;

namespace Fanex.Affiliate.DataService.Importer
{
    public class LBKenoService : XmlImpoterlServiceBase
    {
        protected readonly IApiClient _client;
        protected bool disposed = false;
        public LBKenoService(IApiClient client = null,IAppSettings appSetting = null, IThirdPartyProductRepository thirdPartyProductRepository = null) 
            : base(appSetting, thirdPartyProductRepository)
        {
            _client = client ?? new LBKenoClient();
        }

        public override sealed void Import(string data)
        {
            _thirdPartyProductRepository.ImportData(Products.LBKenoId, data, 0);
        }

        public override IEnumerable<string> GetRawDataList(DateTime startTime, DateTime endTime)
        {
            var paramToRequest = BuildParam(startTime, endTime);

            var data = GetRawData(paramToRequest);
            yield return data;
        }

        private string GetRawData(LBKenoParam paramToRequest)
        {
            var queryValues = BuildQueryParam(paramToRequest);
            var url = _appSetting.GetValue("LBKenoApiUrl");
            var data = _client.GetResponse(url, queryValues);
            return data;

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

        protected override void CheckXmlResponseData(XDocument xml)
        {
            var checker = new LBKenoXmlChecker();
            checker.CheckXml(xml);
        }

        public override XDocument ConvertRawDataToXml(string raw)
        {
            var xml = XmlHelper.ParseToXDocument(raw);
            if (xml == null)
                throw new ThirdPartyProductException($"LBKeno - Can't convert raw data to xml {raw}");
            return xml;
        }

        public override void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool diposing)
        {
            if (disposed)
                return;
            if (diposing)
                _client?.Dispose();
            disposed = true;
        }

        ~LBKenoService()
        {
            Dispose(false);
        }
    }
}
