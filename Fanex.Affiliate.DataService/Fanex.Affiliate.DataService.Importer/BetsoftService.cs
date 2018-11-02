using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Fanex.Affiliate.DataService.Importer.Constants;
using Fanex.Affiliate.DataService.Importer.MetaModels.Betsoft;
using Fanex.Affiliate.DataService.Importer.Repository;
using Fanex.Affiliate.DataService.Importer.Utilities;
using Fanex.Logging;

namespace Fanex.Affiliate.DataService.Importer
{
    public class BetsoftService : IBetsoftService
    {
        private const string Gmt4ZoneId = "SA Western Standard Time";

        private readonly IAppSettings _appSetting;

        private readonly IThirdPartyProductRepository _thirdPartyProductRepository;

        public BetsoftService(IAppSettings appSetting = null, IThirdPartyProductRepository thirdPartyProductRepository = null)
        {
            _appSetting = appSetting ?? new AppSettings();
            _thirdPartyProductRepository = thirdPartyProductRepository ?? new ThirdPartyProductRepository();
        }

        public string GetRawData(BetsoftParam param)
        {
            var apiParam = BuildApiParam(param);
            string url = BuildUrl(apiParam);

            return GetDataString(url);
        }

        public async Task<string> GetRawDataAsync(BetsoftParam param)
        {
            var apiParam = BuildApiParam(param);
            string url = BuildUrl(apiParam);

            return await GetDataStringAsync(url);
        }

        public IList<string> GetMultipleRawDataBatch(BetsoftParam param)
        {
            var result = new List<string>();
            var startDate = new DateTime(param.StartDate.Year, param.StartDate.Month, param.StartDate.Day, param.StartDate.Hour, 0, 0);
            var endDate = new DateTime(param.EndDate.Year, param.EndDate.Month, param.EndDate.Day, param.EndDate.Hour, 0, 0);

            for (var start = startDate; DateTime.Compare(start, endDate) <= 0; start = start.AddHours(1))
            {
                var paramByBatch = BuildParamByBatch(start, endDate);
                result.Add(GetRawData(paramByBatch));
            }

            return result;
        }

        public async Task<IList<string>> GetMultipleRawDataBatchAsync(BetsoftParam param)
        {
            var result = new List<string>();
            var taskList = new List<Task<string>>();
            var startDate = new DateTime(param.StartDate.Year, param.StartDate.Month, param.StartDate.Day, param.StartDate.Hour, 0, 0);
            var endDate = new DateTime(param.EndDate.Year, param.EndDate.Month, param.EndDate.Day, param.EndDate.Hour, 0, 0);

            for (var start = startDate; DateTime.Compare(start, endDate) < 0; start = start.AddHours(1))
            {
                var paramByBatch = BuildParamByBatch(start, endDate);
                taskList.Add(GetRawDataAsync(paramByBatch));
            }

            var taskResultList = await Task.WhenAll(taskList);
            foreach (var resultByBatch in taskResultList)
            {
                result.Add(resultByBatch);
            }

            return result;
        }

        public string ConvertTimeZone(string rawData)
        {
            var xdoc = XDocument.Parse(rawData);
            var response = xdoc.Root.Element("RESPONSE");
            if (response.Element("RESULT").Value.ToLowerInvariant() != "ok")
            {
                LogService.Error($"Betsoft Error:\n{rawData}");
            }

            var accountList = response.Elements("ACCOUNT");
            foreach (var account in accountList)
            {
                var betTicketList = account.Element("GAMESESSION").Elements("BET");

                foreach (var bet in betTicketList)
                {
                    var betTime = bet.Element("BETTIME").Value;
                    bet.Element("BETTIME").Value = DateTimeConverter.ToGMT(DateTime.Parse(betTime), Gmt4ZoneId).ToString("yyyy-MM-dd HH:mm:ss.fff");
                }
            }

            return xdoc.ToString();
        }

        public void Import(string xmlData)
        {
            _thirdPartyProductRepository.ImportData(Products.BetsoftProductId, xmlData);
        }

        public void Import(BetsoftParam param)
        {
            var rawData = GetMultipleRawDataBatchAsync(param).Result;

            foreach (var dataItem in rawData)
            {
                var xmlData = ConvertTimeZone(dataItem);
                Import(xmlData);
            }
        }

        private static BetsoftParam BuildParamByBatch(DateTime start, DateTime end)
        {
            var endDateByBatch = start.AddHours(1);

            return new BetsoftParam
            {
                StartDate = start,
                EndDate = DateTime.Compare(endDateByBatch, end) > 0 ? end : endDateByBatch
            };
        }

        private BetsoftParam BuildApiParam(BetsoftParam param)
        {
            var start = new DateTime(param.StartDate.Year, param.StartDate.Month, param.StartDate.Day, param.StartDate.Hour, 0, 0, DateTimeKind.Local);
            var end = new DateTime(param.EndDate.Year, param.EndDate.Month, param.EndDate.Day, param.EndDate.Hour, 0, 0, DateTimeKind.Local);

            return new BetsoftParam
            {
                StartDate = start.ToUniversalTime(),
                EndDate = end.ToUniversalTime(),
                ApiUrl = _appSetting.BetsoftApiUrl,
                BankId = _appSetting.BetsoftBankId,
                Password = _appSetting.BetsoftPassword
            };
        }

        private string BuildUrl(BetsoftParam param)
        {
            var startDate = param.StartDate.ToString(_appSetting.BetsoftFormatDateTime);
            var endDate = param.EndDate.ToString(_appSetting.BetsoftFormatDateTime);
            var inputToHash = $"{startDate}{endDate}{param.BankId}{param.Password}";
            var hashedString = GenerateHash(inputToHash);

            return $"{param.ApiUrl}?startDate={startDate}&endDate={endDate}&bankId={param.BankId}&hash={hashedString}";
        }

        private static string GenerateHash(string input)
        {
            var hash = new StringBuilder();
            using (var md5provider = new MD5CryptoServiceProvider())
            {
                var bytes = md5provider.ComputeHash(Encoding.UTF8.GetBytes(input));

                for (int i = 0; i < bytes.Length; i++)
                {
                    hash.Append(bytes[i].ToString("x2"));
                }

                return hash.ToString();
            }
        }

        private string GetDataString(string url)
        {
            using (var client = new WebClient())
            {
                return client.DownloadString(url);
            }
        }

        private async Task<string> GetDataStringAsync(string url)
        {
            using (var client = new WebClient())
            {
                return await client.DownloadStringTaskAsync(url);
            }
        }
    }
}