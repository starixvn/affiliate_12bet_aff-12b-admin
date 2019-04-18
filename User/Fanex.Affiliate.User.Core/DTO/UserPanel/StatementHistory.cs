using System;

namespace Fanex.Affiliate.User.Core.DTO.UserPanel
{
    public class StatementHistory
    {
        public DateTime MonthDate { get; set; }

        public byte TransType { get; set; }

        public decimal Amount { get; set; }

        public string Status { get; set; }

        public string Description { get; set; }

        public string DateString
        {
            get
            {
                return TransType == (byte)Enum.TransType.Payment || TransType == (byte)Enum.TransType.Adjustment ?
                    MonthDate.ToString("MM-dd-yyyy") :
                    MonthDate.ToString("MMMM-yyyy");
            }
        }

        public string DisplayStatus
        {
            get
            {
                switch (Status.ToLowerInvariant())
                {
                    case "running":
                    case "pending":
                        return Resources.Lang.Proccessing;

                    case "completed":
                        return Resources.Lang.Approved;

                    case "cancelled":
                        return Resources.Lang.Cancelled;

                    case "approved":
                        return Resources.Lang.Approved;

                    case "rejected":
                        return Resources.Lang.Rejected;

                    case "current":
                        return Resources.Lang.Current;

                    default:
                        return Status;
                }
            }
        }

        public string TransTypeString
        {
            get
            {
                switch (TransType)
                {
                    case Constants.TransType.Adjustment:
                        return Resources.Lang.Adjustment;
                    case Constants.TransType.Commission:
                        return Resources.Lang.MonthlyWinlost;
                    case Constants.TransType.Payment:
                        return Resources.Lang.PaymentRequest;

                    default:
                        return Resources.Lang.Transaction;
                }
            }
        }
    }
}