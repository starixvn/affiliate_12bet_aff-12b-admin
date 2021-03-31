using Newtonsoft.Json;
using System;

namespace Fanex.Affiliate.Admin.Core.DTO.NewViewLog
{
    public class NewViewLogBasicInfo
    {
        public string Action { get; set; }

        public string UpdatedBy { get; set; }

        [JsonIgnore]
        public DateTime CreatedDate { get; set; }

        public string CreatedDateText => CreatedDate.ToString("MM/dd/yyyy hh:mm:ss tt");
    }
}
