using Fanex.Affiliate.DataService.Importer.Core.Constants;
using Fanex.Affiliate.DataService.Importer.Core.Utilities;
using Fanex.Affiliate.ThirdPartyProduct.Constants;
using Fanex.Affiliate.ThirdPartyProduct.MetaModels;
using Fanex.Affiliate.ThirdPartyProduct.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Fanex.Affiliate.ThirdPartyProduct.Utilities
{
    public static class Helpers
    {
        public static Tuple<DateTime, DateTime> GetTime(OpusRequestParam param)
        {
            var startTime = DateTime.ParseExact(param.StartTime, Format.RequestDateTime, CultureInfo.InvariantCulture);
            var endTime = DateTime.ParseExact(param.EndTime, Format.RequestDateTime, CultureInfo.InvariantCulture);

            if (param.IsGetByApiTimeZone)
            {
                startTime = DateTimeConverter.ConvertTime(startTime, TimeZoneId.GmtPlus8, TimeZoneInfo.Local.Id);
                endTime = DateTimeConverter.ConvertTime(endTime, TimeZoneId.GmtPlus8, TimeZoneInfo.Local.Id);
            }

            return Tuple.Create(startTime, endTime);
        }

        public static IEnumerable<string> GetDataByUserName(IEnumerable<string> rawDataList, string userName, bool isOpus)
        {
            var dataList = new List<string>();

            foreach (var rawData in rawDataList)
            {
                var xdoc = XDocument.Parse(rawData);
                var betHistoryList = isOpus ? xdoc.Root.Element("bets").Elements("row") : xdoc.Root.Elements("history");

                if(isOpus)
                {
                    betHistoryList.Where(x => !string.Equals(x.Element("member_code").Value, userName, StringComparison.InvariantCultureIgnoreCase)).Remove();
                } else
                {
                    betHistoryList.Where(x => !string.Equals(x.Attribute("userId").Value, userName, StringComparison.InvariantCultureIgnoreCase)).Remove();
                }

                if (betHistoryList.Any())
                {
                    dataList.Add(xdoc.ToString());
                }
            }

            return dataList;
        }

        public static string Convert(StringBuilder logBuilder, StringBuilder rawDataBuilder)
        {
            return JsonConvert.SerializeObject(new ThirdPartyProductViewModel
            {
                LogMessage = logBuilder.ToString(),
                RawData = rawDataBuilder.ToString()
            });
        }
    }
}
