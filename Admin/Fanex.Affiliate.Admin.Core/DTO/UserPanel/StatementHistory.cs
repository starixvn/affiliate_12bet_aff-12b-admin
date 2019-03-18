using Newtonsoft.Json;
using System;

namespace Fanex.Affiliate.Admin.Core.DTO.UserPanel
{
    public class StatementHistory
    {
        public long TransId { get; set; }

        [JsonIgnore]
        public DateTime Date { get; set; }

        public int TransType { get; set; }

        public decimal Amount { get; set; }

        public string Status { get; set; }

        public string Creator { get; set; }

        public string Description { get; set; }

        public string DateText => Date.ToString("MMM-yyyy");
    }
}
