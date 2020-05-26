using Newtonsoft.Json;
using System;

namespace Fanex.Affiliate.ThirdPartyProduct.DTO
{
    public class TimeTracker
    {
        public int Currency { get; set; }

        [JsonIgnore]
        public DateTime CallTime { get; set; }

        public string CallTimeText => CallTime.ToString("MM/dd/yyyy HH:mm:ss");
    }
}
