using Newtonsoft.Json;
using System;

namespace Fanex.Affiliate.Admin.Core.DTO.UserPanel
{
    [Serializable]
    public class NewViewLog
    {
        public int Id { get; set; }

        [JsonIgnore]
        public DateTime Date { get; set; }

        public string UpdatedBy { get; set; }

        public string OldData { get; set; }

        public string NewData { get; set; }

        public string Action { get; set; }

        public string Ip { get; set; }

        public string DateText => Date.ToString("MM/dd/yyyy hh:mm:ss tt");
    }
}
