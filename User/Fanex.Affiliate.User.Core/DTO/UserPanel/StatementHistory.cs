using Fanex.Affiliate.User.Core.Resources;
using System;
using System.Collections.Generic;

namespace Fanex.Affiliate.User.Core.DTO.UserPanel
{
    public class StatementHistory
    {
        private static readonly IDictionary<string, string> StatusList = new Dictionary<string, string>
        {
            ["running"] = Lang.Proccessing,
            ["pending"] = Lang.Proccessing,
            ["completed"] = Lang.Approved,
            ["cancelled"] = Lang.Cancelled,
            ["approved"] = Lang.Approved,
            ["rejected"] = Lang.Rejected,
            ["current"] = Lang.Current
        };

        public DateTime MonthDate { get; set; }

        public int TransType { get; set; }

        public decimal Amount { get; set; }

        public string Status { get; set; }

        public string Description { get; set; }

        public string DateString
        {
            get
            {
                return TransType == (int)Enum.TransType.Payment || TransType == (int)Enum.TransType.Adjustment ?
                    MonthDate.ToString("MM-dd-yyyy") :
                    MonthDate.ToString("MMMM-yyyy");
            }
        }

        public string DisplayStatus
        {
            get
            {
                var lowerStatus = Status.ToLowerInvariant();

                return StatusList.ContainsKey(lowerStatus) ? StatusList[lowerStatus] : Status;
            }
        }

        public string TransTypeString
        {
            get
            {
                switch (TransType)
                {
                    case Constants.TransType.Adjustment:
                        return Lang.Adjustment;
                    case Constants.TransType.Commission:
                        return Lang.MonthlyWinlost;
                    case Constants.TransType.Payment:
                        return Lang.PaymentRequest;

                    default:
                        return Lang.Transaction;
                }
            }
        }
    }
}