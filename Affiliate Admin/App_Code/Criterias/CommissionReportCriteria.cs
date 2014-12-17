using System;

namespace Criterias
{
    public class CommissionReportCriteria
    {
        public string Username { get; set; }

        public string PreferredLanguage { get; set; }

        public int Sport { get; set; }

        public int Month { get; set; }

        public int Year { get; set; }

        public int SortBy { get; set; }

        public bool IsDesc { get; set; }

        public int PageSize { get; set; }

        public int PageIndex { get; set; }

        public int NRows { get; set; }

        public int TotalPages
        {
            get
            {
                var quotient = NRows / PageSize;
                var remainder = NRows % PageSize;
                return remainder == 0 ? quotient : quotient + 1;
            }
        }

        public string SpName
        {
            get
            {
                return "Aff_AffiliateCommReport" + AppSettings.SuffixSp;
            }
        }
    }
}