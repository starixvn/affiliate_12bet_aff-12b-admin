using System;

namespace Criterias
{
    public class AffiliateReportCriteria
    {
        public string PreferredLanguage { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public int SportId { get; set; }

        public bool IsGetAll { get; set; }

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
                return "Aff_AffiliateReport";
            }
        }
    }
}
