using System;

namespace Criterias
{
    public class SBPlayerTurnoverReportCriteria
    {
        public string PreferredLanguage { get; set; }

        public DateTime DtmFDate { get; set; }

        public DateTime DtmTDate { get; set; }

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
                //return "Aff_PlayerSportsBookReport";
                return "Aff_PlayerSportsBookReport_xtest";
            }
        }
    }
}