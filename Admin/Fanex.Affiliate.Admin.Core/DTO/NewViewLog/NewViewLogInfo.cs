
using Newtonsoft.Json;
using System;

namespace Fanex.Affiliate.Admin.Core.DTO.NewViewLog
{
    public class NewViewLogInfo
    {
        public int AffId { get; set; }

        public string AffName { get; set; }

        public int NumberOfAction { get; set; }

        public string LastAction { get; set; }

        public string Actor { get; set; }

        [JsonIgnore]
        public DateTime DateTime { get; set; }

        public string DateTimeText => this.DateTime.ToString("MM/dd/yyyy hh:mm:ss tt");
    }
}
