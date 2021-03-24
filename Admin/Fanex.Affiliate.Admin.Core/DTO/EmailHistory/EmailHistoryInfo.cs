using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using Newtonsoft.Json;
using System;

namespace Fanex.Affiliate.Admin.Core.DTO.EmailHistory
{
    [Serializable]
    public class EmailHistoryInfo
    {
        public int EmailId { get; set; }

        [PermissionCheck(Function.AffPersonalInfo)]
        public string Sender { get; set; }

        public string AffName { get; set; }

        public string Subject { get; set; }

        public string Email { get; set; }

        [JsonIgnore]
        public DateTime SentDate { get; set; }

        public string SentDateText => SentDate.ToString("MM/dd/yyyy HH:mm:ss");
    }
}
