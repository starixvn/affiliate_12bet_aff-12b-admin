using Newtonsoft.Json;
using System;

namespace Fanex.Affiliate.Admin.Core.DTO.Message
{
    public class MessageInfo
    {
        public int MessageId { get; set; }

        [JsonIgnore]
        public string CreatorName { get; set; }

        public string Creator => CreatorName;

        [JsonIgnore]
        public string Message_En { get; set; }

        public string Message => Message_En;

        public bool Important { get; set; }

        [JsonIgnore]
        public string UpdatedName { get; set; }

        public string UpdatedBy => UpdatedName;

        [JsonIgnore]
        public DateTime? PostedDate { get; set; }

        [JsonIgnore]
        public DateTime? UpdatedDate { get; set; }

        public string PostedDateText => PostedDate?.ToString("MM/dd/yyyy HH:mm:ss");

        public string UpdatedDateText => UpdatedDate?.ToString("MM/dd/yyyy HH:mm:ss");
    }
}
