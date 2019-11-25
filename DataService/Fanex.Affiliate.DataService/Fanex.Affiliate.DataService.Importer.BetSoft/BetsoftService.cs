using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Fanex.Affiliate.DataService.Importer.Core.Constants;
using Fanex.Affiliate.DataService.Importer.Core.Exceptions;
using Fanex.Affiliate.DataService.Importer.Betsoft.MetaModels;
using Fanex.Affiliate.DataService.Importer.Core.Repository;
using Fanex.Affiliate.DataService.Importer.Core.Utilities;
using Fanex.Logging;

namespace Fanex.Affiliate.DataService.Importer
{
    public class BetsoftService : IBetsoftService
    {
        private readonly IAppSettings _appSetting;

        private readonly IThirdPartyProductRepository _thirdPartyProductRepository;

        public BetsoftService(IAppSettings appSetting = null, IThirdPartyProductRepository thirdPartyProductRepository = null)
        {
            _appSetting = appSetting ?? new AppSettings();
            _thirdPartyProductRepository = thirdPartyProductRepository ?? new ThirdPartyProductRepository();
        }

        public async Task<string> GetRawDataAsync(BetsoftParam param)
        {
            var apiParam = BuildApiParam(param);
            var url = BuildUrl(apiParam);

            return await GetDataStringAsync(url);
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

            result.AddRange(await Task.WhenAll(taskList));

            return result;
        }

        public void Import(string xmlData)
        {
            _thirdPartyProductRepository.ImportData(Products.BetsoftProductId, xmlData);
        }

        public void Import(BetsoftParam param)
        {
            ImportByBatch(param);
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
                ApiUrl = _appSetting.GetValue("BetsoftApiUrl"),
                BankId = _appSetting.GetValue("BetsoftBankId"),
                Password = _appSetting.GetValue("BetsoftPassword")
            };
        }

        private string BuildUrl(BetsoftParam param)
        {
            var format = _appSetting.GetValue("BetsoftFormatDateTime");
            var startDate = param.StartDate.ToString(format);
            var endDate = param.EndDate.ToString(format);
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

        private async Task<string> GetDataStringAsync(string url)
        {
            using (var client = new WebClient())
            {
                return await client.DownloadStringTaskAsync(url);
            }
        }

        private void ImportByBatch(BetsoftParam param)
        {
            var startDate = new DateTime(param.StartDate.Year, param.StartDate.Month, param.StartDate.Day, param.StartDate.Hour, 0, 0);
            var endDate = new DateTime(param.EndDate.Year, param.EndDate.Month, param.EndDate.Day, param.EndDate.Hour, 0, 0);

            for (var start = startDate; DateTime.Compare(start, endDate) < 0; start = start.AddHours(1))
            {
                var paramByBatch = BuildParamByBatch(start, endDate);
                var rawData = GetRawData(paramByBatch);
                CheckRawData(rawData);
                Import(rawData);
            }
        }

        private string GetRawData(BetsoftParam param)
        {
            var apiParam = BuildApiParam(param);
            var url = BuildUrl(apiParam);

            using (var client = new WebClient())
            {
                return client.DownloadString(url);
            }
        }

        private void CheckRawData(string rawData)
        {
            var xdoc = XmlHelper.ParseToXDocument(rawData);
            if (xdoc == null)
            {
                throw new ThirdPartyProductException($"Error: Betsoft - xDocument is null\n{rawData}");
            }

            var response = xdoc.Root.Element("RESPONSE");
            if (!string.Equals("ok", response.Element("RESULT").Value, StringComparison.InvariantCultureIgnoreCase))
            {
                LogService.Error($"Betsoft Error:\n{rawData}");
            }
        }
    }
}