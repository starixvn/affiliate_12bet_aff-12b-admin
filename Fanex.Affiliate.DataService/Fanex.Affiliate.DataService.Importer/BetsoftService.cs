using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Fanex.Affiliate.DataService.Importer.DTO.Betsoft;
using Fanex.Affiliate.DataService.Importer.MetaModels.Betsoft;
using Fanex.Affiliate.DataService.Importer.Utilities;

namespace Fanex.Affiliate.DataService.Importer
{
    public class BetsoftService : IBetsoftService
    {
        public const string Gmt4ZoneId = "SA Western Standard Time";

        public readonly IAppSettings _appSetting;

        public BetsoftService(IAppSettings appSetting = null)
        {
            _appSetting = appSetting ?? new AppSettings();
        }

        public string GetRawData(BetsoftParam param)
        {
            string url = BuildUrl(param);

            return GetDataString(url);
        }

        public async Task<string> GetRawDataAsync(BetsoftParam param)
        {
            string url = BuildUrl(param);

            return await GetDataStringAsync(url);
        }

        public IList<string> GetMultipleRawDataBatch(BetsoftParam param)
        {
            var result = new List<string>();
            var startDate = param.StartDate;
            var endDate = param.EndDate;

            for (var start = startDate; DateTime.Compare(start, endDate) <= 0; start = start.AddHours(1))
            {
                BetsoftParam paramByBatch = BuildParamByBatch(param, endDate, start);
                result.Add(GetRawData(paramByBatch));
            }

            return result;
        }

        public async Task<IList<string>> GetMultipleRawDataBatchAsync(BetsoftParam param)
        {
            var result = new List<string>();
            var taskList = new List<Task<string>>();
            var startDate = new DateTime(param.StartDate.Year, param.StartDate.Month, param.StartDate.Day, param.StartDate.Hour, param.StartDate.Minute, param.StartDate.Second);
            var endDate = new DateTime(param.EndDate.Year, param.EndDate.Month, param.EndDate.Day, param.EndDate.Hour, param.EndDate.Minute, param.EndDate.Second);

            for (var start = startDate; DateTime.Compare(start, endDate) < 0; start = start.AddHours(1))
            {
                var paramByBatch = BuildParamByBatch(param, endDate, start);
                taskList.Add(GetRawDataAsync(paramByBatch));
            }

            var taskResultList = await Task.WhenAll(taskList);
            foreach (var resultByBatch in taskResultList)
            {
                result.Add(resultByBatch);
            }

            return result;
        }

        private static BetsoftParam BuildParamByBatch(BetsoftParam param, DateTime endDate, DateTime start)
        {
            var endDateByBatch = start.AddHours(1);

            return new BetsoftParam
            {
                ApiUrl = param.ApiUrl,
                BankId = param.BankId,
                Password = param.Password,
                StartDate = start,
                EndDate = DateTime.Compare(endDateByBatch, endDate) > 0 ? endDate : endDateByBatch
            };
        }

        public IList<BetsoftDataItem> ConvertToDataList(string rawData)
        {
            var result = new List<BetsoftDataItem>();
            var xdoc = XDocument.Parse(rawData);

            var accountList = xdoc.Root.Element("RESPONSE").Elements("ACCOUNT");
            foreach (var account in accountList)
            {
                var betTicketList = account.Element("GAMESESSION").Elements("BET")
                        .Select(betTicket => new BetsoftDataItem
                        {
                            UserName = account.Element("USERNAME").Value,
                            Amount = Convert.ToDecimal(betTicket.Element("AMOUNT").Value),
                            Win = Convert.ToDecimal(betTicket.Element("WIN").Value),
                            WinLossDate = DateTime.Parse(betTicket.Element("BETTIME").Value)
                        });

                result.AddRange(betTicketList);
            }

            return result;
        }

        public void Import(IList<BetsoftDataItem> dataList)
        {
            var builder = new StringBuilder();
            builder.AppendLine("<root>");
            foreach (var item in dataList)
            {
                var convertedDateTime = ToGMT4(item.WinLossDate);
                builder.AppendLine($"<trans username=\"{item.UserName}\" amount=\"{item.Amount}\" win=\"{item.Win}\" winlossdate=\"{convertedDateTime}\" />");
            }
            builder.AppendLine("</root>");

            var xmlString = builder.ToString();
            throw new NotImplementedException();
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

        private static DateTime ToGMT4(DateTime value)
        {
            var utcDate = DateTime.SpecifyKind(value, DateTimeKind.Utc);
            var timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(Gmt4ZoneId);
            return TimeZoneInfo.ConvertTimeFromUtc(utcDate, timeZoneInfo);
        }
    }
}