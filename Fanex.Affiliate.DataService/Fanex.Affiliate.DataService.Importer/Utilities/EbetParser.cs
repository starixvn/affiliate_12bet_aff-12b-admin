using System;
using System.Linq;
using Fanex.Affiliate.DataService.Importer.DTO.Ebet;
using Newtonsoft.Json.Linq;

namespace Fanex.Affiliate.DataService.Importer.Utilities
{
    public static class EbetParser
    {
        public static EbetSummary FromJson(string jsonData)
        {
            var jsonObject = JObject.Parse(jsonData);

            return new EbetSummary
            {
                ResultInfo = GetEbetResultInfo(jsonObject),
                BetHistories = jsonObject["betHistories"].Children().Select(x => new EbetHistory
                {
                    BetHistoryId = Convert.ToString(x["betHistoryId"]),
                    UserName = Convert.ToString(x["username"]),
                    Bet = Convert.ToDecimal(x["bet"]),
                    PayoutUnixTime = Convert.ToInt64(x["payoutTime"]),
                    Payout = Convert.ToDecimal(x["payout"])
                })
            };
        }

        public static EbetResultInfo GetEbetResultInfo(string jsonData)
        {
            var jsonObject = JObject.Parse(jsonData);

            return GetEbetResultInfo(jsonObject);
        }

        private static EbetResultInfo GetEbetResultInfo(JObject jsonObject)
        {
            return new EbetResultInfo
            {
                Status = Convert.ToInt32(jsonObject["status"]),
                RemainingVisits = Convert.ToInt32(jsonObject["remainingVisits"]),
                Count = Convert.ToInt32(jsonObject["count"])
            };
        }
    }
}
