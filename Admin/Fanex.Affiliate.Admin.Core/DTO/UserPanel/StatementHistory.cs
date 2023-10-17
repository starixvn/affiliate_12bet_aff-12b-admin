using Newtonsoft.Json;
using System;
using System.Collections.Generic;

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

        [JsonIgnore]
        public string Description { get; set; }

        public string DateText => (TransType == Constants.TransType.Adjustment || TransType == Constants.TransType.PaymentRequest)
                                    ? Date.ToString("dd-MMM-yyyy") : Date.ToString("MMM-yyyy");

        public string NormalizeDateText => Date.ToString("yyyy-MM-dd");

        public string DescriptionDisplay
        {
            get
            {
                if (TransType == Constants.TransType.MonthlyTotal)
                {
                    return GetMonthlyTotalDescription(Description);
                }

                return Description;
            }
        }

        public IEnumerable<StatementHistory> Children { get; set; } = new List<StatementHistory>();

        private static string GetMonthlyTotalDescription(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
            {
                return "Monthly Total";
            }

            if (description.Contains("Current"))
            {
                return "Current Month Statistics";
            }
            if (description.Contains("Uncalculated"))
            {
                return "Uncalculated Month";
            }  
            
            return "Monthly Total";
        }
    }
}
