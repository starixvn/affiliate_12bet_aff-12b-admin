using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
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

        public static QuartzSummary FromJsonObject(JObject jsonObject)
        {
            return new QuartzSummary
            {
                ResultInfo = GetResultInfo(jsonObject),
                BetHistoryList = GetHistoryList(jsonObject)
            };
        }

        public static IEnumerable<QuartzBetHistory> GetHistoryList(JObject jsonObject)
        {
            var result = new List<QuartzBetHistory>();

            var reportList = jsonObject["report"];

            if (reportList != null)
            {
                foreach (var reportItem in reportList.Children())
                {
                    var playerId = reportItem.Value<string>("playerid");
                    var currency = reportItem.Value<string>("currency");

                    foreach (var s in reportItem["sessions"].Children())
                    {
                        var sessionId = s.Value<string>("sessionid");
                        var gameId = s.Value<string>("gameid");

                        foreach (var r in s["rounds"].Children())
                        {
                            var roundId = r.Value<string>("roundid");

                            result.AddRange(r["transactions"].Children().Select(t => new QuartzBetHistory
                            {
                                PlayerId = playerId,
                                Currency = currency,
                                SessionId = sessionId,
                                GameId = gameId,
                                RoundId = roundId,
                                TransactionId = t.Value<string>("transactionid"),
                                Type = t.Value<string>("type"),
                                Time = DateTime.ParseExact(t.Value<string>("time"), "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture),
                                Amount = t.Value<decimal>("amount")
                            }));
                        }
                    }
                }
            }

            return result;
        }
    }
}