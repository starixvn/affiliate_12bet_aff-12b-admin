using System;
using Newtonsoft.Json;

namespace Fanex.Affiliate.ThirdPartyProduct.DTO
{
    public class ImportQueue
    {
        public int QueueId { get; set; }

        [JsonIgnore]
        public DateTime CheckInTime { get; set; }

        public string LogDesc { get; set; }

        public string CheckInTimeText => CheckInTime.ToString("MM/dd/yyyy HH:mm:ss");
    }
}
