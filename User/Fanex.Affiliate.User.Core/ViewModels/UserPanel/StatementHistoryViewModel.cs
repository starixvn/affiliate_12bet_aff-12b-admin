using System;
using System.Collections.Generic;

namespace Fanex.Affiliate.User.Core.ViewModels.UserPanel
{
    public class StatementHistoryViewModel
    {
        public DateTime MonthDate { get; set; }

        public int TransType { get; set; }

        public decimal Amount { get; set; }

        public string Status { get; set; }

        public string Description { get; set; }

        public string DateString
        {
            get
            {
                return TransType == Constants.TransType.Payment
                    || TransType == Constants.TransType.Adjustment
                    ? MonthDate.ToString("MM-dd-yyyy")
                    : MonthDate.ToString("MMMM-yyyy");
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
                    case Constants.TransType.MonthlyEarning:
                        return Resources.Lang.MonthlyEarning;
                    case Constants.TransType.Payment:
                        return Resources.Lang.PaymentRequest;
                    case Constants.TransType.NewFDM:
                        return Resources.Lang.NewFDM;
                    case Constants.TransType.MonthlyTotal:
                        {
                            if (string.Equals("Monthly Earning", Description, StringComparison.OrdinalIgnoreCase))
                            {
                                return Resources.Lang.MonthlyTotal;
                            }

                            return Resources.Lang.TransactionInReport;
                        }
                    default:
                        return Resources.Lang.TransactionInReport;
                }
            }
        }

        public string DescriptionDisplay
        {
            get
            {
                switch (TransType)
                {
                    case Constants.TransType.MonthlyEarning:
                        return GetMonthlyEarningDescription(Description);
                    case Constants.TransType.NewFDM:
                        return GetNewFdmDescription(Description);
                    case Constants.TransType.MonthlyTotal:
                        return GetMonthlyTotalDescription(Description);
                    default:
                        return Description;
                }
            }
        }

        public IEnumerable<StatementHistoryViewModel> Children { get; set; } = new List<StatementHistoryViewModel>();

        private static string GetMonthlyEarningDescription(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
            {
                return Resources.Lang.MonthlyEarning;
            }

            if (description.Contains("Current"))
            {
                return Resources.Lang.CurrentMonthlyEarning;
            }
            if (description.Contains("Uncalculated"))
            {
                return Resources.Lang.UncalculatedMonthlyEarning;
            }

            return Resources.Lang.MonthlyEarning;
        }

        private static string GetNewFdmDescription(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
            {
                return Resources.Lang.NewFdmCommBonus;
            }

            if (description.Contains("Current"))
            {
                return Resources.Lang.CurrentNewFdmCommBonus;
            }
            if (description.Contains("Uncalculated"))
            {
                return Resources.Lang.UncalculatedNewFdmCommBonus;
            }

            return Resources.Lang.NewFdmCommBonus;
        }

        private static string GetMonthlyTotalDescription(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
            {
                return Resources.Lang.MonthlyTotal;
            }

            if (description.Contains("Current"))
            {
                return Resources.Lang.CurrentMonthStatistics;
            }
            if (description.Contains("Uncalculated"))
            {
                return Resources.Lang.UncalculatedMonth;
            }

            return Resources.Lang.MonthlyTotal;
        }
    }
}
