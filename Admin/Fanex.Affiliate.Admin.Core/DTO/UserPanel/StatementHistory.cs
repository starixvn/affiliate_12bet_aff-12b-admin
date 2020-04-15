using Fanex.Affiliate.Admin.Core.Constants;
using Newtonsoft.Json;
using System;

namespace Fanex.Affiliate.Admin.Core.DTO.UserPanel
{
    [Serializable]
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

        public string DateText => (TransType == Code.MonthlyWinlost || TransType == Code.Transaction) ? Date.ToString("MMM-yyyy") : Date.ToString("dd-MMM-yyyy");

        public string NormalizeDateText => Date.ToString("yyyy-MM-dd");
    }
}
