using Newtonsoft.Json;
using System;

namespace Fanex.Affiliate.Admin.Core.DTO.Home
{
    public class OnlineListInfo
    {
        public string UserName { get; set; }

        [JsonIgnore]
        public DateTime? LoggedIn { get; set; }

        [JsonIgnore]
        public DateTime? LastRequest { get; set; }

        public string Ip { get; set; }

        public string Role { get; set; }

        public string LoggedInText => LoggedIn?.ToString("MM/dd/yyyy HH:mm:ss");

        public string LastRequestText => LastRequest?.ToString("MM/dd/yyyy HH:mm:ss");
    }
}
