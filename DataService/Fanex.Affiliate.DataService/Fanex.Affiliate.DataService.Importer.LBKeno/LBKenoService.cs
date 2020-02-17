using Fanex.Affiliate.DataService.Importer.Core.Constants;
using Fanex.Affiliate.DataService.Importer.Core.Infrastructure;
using Fanex.Affiliate.DataService.Importer.Core.Enum;
using Fanex.Affiliate.DataService.Importer.Core.Exceptions;
using Fanex.Affiliate.DataService.Importer.LBKeno;
using Fanex.Affiliate.DataService.Importer.LBKeno.Client;
using Fanex.Affiliate.DataService.Importer.LBKeno.MetaModels;
using Fanex.Affiliate.DataService.Importer.Core.Repository;
using Fanex.Affiliate.DataService.Importer.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Globalization;

namespace Fanex.Affiliate.DataService.Importer
{
    public class LBKenoService : XmlImpoterlServiceBase
    {

        private const string TimeDataFormat = "yyyy-MM-dd HH:mm:ss";

        protected readonly IApiClient _client;

        protected override int ProductId => Products.LBKenoId;

        public override bool IsNeedConvertTime => true;

        public override int HoursAjust => -12;

        public LBKenoService(IApiClient client = null,IAppSettings appSetting = null, IThirdPartyProductRepository thirdPartyProductRepository = null) 
            : base(appSetting, thirdPartyProductRepository)
        {
            _client = client ?? new LBKenoClient();
        }


        public override async Task<IEnumerable<string>> GetRawDataListAsync(DateTime startTime, DateTime endTime)
        {
            var paramToRequest = BuildParam(startTime, endTime);
            var datas = new List<string>();

            datas.Add(await GetRawDataAsync(paramToRequest).ConfigureAwait(false));
            return datas;
        }

        private async Task<string> GetRawDataAsync(LBKenoParam paramToRequest)
        {
            var queryValues = BuildQueryParam(paramToRequest);
            var url = _appSetting.GetValue("LBKenoApiUrl");
            return await _client.GetResponseAsync(url, queryValues).ConfigureAwait(false);

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
            queryValues.Add("status", paramToRequest.Status.ToString().ToLower(CultureInfo.InvariantCulture));
            return queryValues;
        }

        private LBKenoParam BuildParam(DateTime startTime, DateTime endTime)
        {
            return new LBKenoParam
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

        public override Task<XDocument> ConvertRawDataToXmlAsync(string raw)
        {
            var xml = XmlHelper.ParseToXDocument(raw);
            if (xml == null)
                throw new ThirdPartyProductException($"LBKeno - Can't convert raw data to xml {raw}");
            return Task.FromResult(xml);
        }

        protected override void EnsureEndTimeLaterThanStartTime(DateTime startTime, DateTime endTime)
        {
            base.EnsureEndTimeLaterThanStartTime(startTime, endTime);
            if (startTime.Year == endTime.Year && startTime.Month == endTime.Month && startTime.Day == endTime.Day)
                throw new ThirdPartyProductException("LBKeno - start time day equal to end time day - Can't query api with the same day");
        }

        protected override XDocument ConvertTimeZone(XDocument xml)
        {
            var items = xml.Root.Element("bet").Elements("item");
            foreach (var item in items)
            {
                UpdateTimeZoneData("bet_time", item);
                UpdateTimeZoneData("trans_time", item);
            }
            return xml;
        }

        private static void UpdateTimeZoneData(string elementName, XElement element)
        {
            var timeString = element.Attribute(elementName)?.Value;
            if (!string.IsNullOrEmpty(timeString))
            {
                var time = DateTime.ParseExact(timeString, TimeDataFormat, CultureInfo.InvariantCulture);
                var convertedTime = DateTimeConverter.ConvertTime(time, TimeZoneId.GmtPlus8, TimeZoneId.GmtMinus4);
                element.Attribute(elementName).Value = convertedTime.ToString(TimeDataFormat);
            }
        }
    }
}
