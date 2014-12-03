using System;

namespace Criterias
{
    public class QMReportCriteria
    {
        public string Username { get; set; }

        public string PreferredLanguage { get; set; }

        public int Month { get; set; }

        public int Year { get; set; }

        public int SortBy { get; set; }

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

        public bool IsFavorite { get; set; }

        public string SpName
        {
            get
            {
                return "Aff_ReportQM";
            }
        }
    }
}