using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
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

        public string GetData(BetsoftParam param)
        {
            var inputToHash = $"{param.StartDate}{param.EndDate}{param.BankId}{param.Password}";
            var hashedString = GenerateHash(inputToHash);
            var url = $"{param.ApiUrl}?startDate={param.StartDate}&endDate={param.EndDate}&bankId={param.BankId}&hash={hashedString}";

            return GetDataString(url);
        }

        public IList<BetsoftDataItem> ConvertToDataList(string xmlData)
        {
            var result = new List<BetsoftDataItem>();
            var xdoc = XDocument.Parse(xmlData);

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

        private static string GenerateHash(string input)
        {
            var hash = new StringBuilder();
            var md5provider = new MD5CryptoServiceProvider();
            var bytes = md5provider.ComputeHash(System.Text.Encoding.UTF8.GetBytes(input));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }

        private string GetDataString(string url)
        {
            using (var client = new WebClient())
            {
                return client.DownloadString(url);
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