using System;
using System.Globalization;
using Fanex.Affiliate.DataService.Importer.DTO.Quartz;
using Newtonsoft.Json.Linq;

namespace Fanex.Affiliate.DataService.Importer.Utilities
{
    public static class QuartzParser
    {
        public static QuartzResultInfo GetResultInfo(string jsonData)
        {
            var jsonObject = JObject.Parse(jsonData);

            return GetResultInfo(jsonObject);
        }

        public static string GetStatus(string jsonData)
        {
            var jsonObject = JObject.Parse(jsonData);

            return Convert.ToString(jsonObject["status"]);
        }

        public static QuartzResultInfo GetResultInfo(JObject jsonObject)
        {
            return new QuartzResultInfo
            {
                DateTime = DateTime.ParseExact(Convert.ToString(jsonObject["datetime"]), "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture),
                Pages = Convert.ToInt32(jsonObject["pages"]),
                Total = Convert.ToInt32(jsonObject["total"]),
                CurrentPage = Convert.ToInt32(jsonObject["currentpage"])
            };
        }
    }
}
