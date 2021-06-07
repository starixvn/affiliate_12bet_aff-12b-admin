using Newtonsoft.Json;
using System;

namespace Fanex.Affiliate.Admin.Core.DTO.Member
{
    public class RegisteredMemberInfo
    {
        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [JsonIgnore]
        public DateTime? CreatedDate { get; set; }

        public string AffName { get; set; }

        public string Currency { get; set; }

        public string CreatedDateText => CreatedDate?.ToString("MM/dd/yyyy HH:mm:ss");
    }
}
