namespace Criterias
{
    public class AffiliateListCriteria
    {
        public int UserId
        {
            get { return AppSettings.Session.CustID; }
        }

        public int GroupId { get; set; }

        public string Status { get; set; }

        public int AffId { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public bool IsClosed { get; set; }

        public string Language { get; set; }

        public int PageSize { get; set; }

        public int PageIndex { get; set; }

        public bool IsAll { get; set; }

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
            get { return IsFavorite ? "Aff_AffiliateSelFavorite" : "Aff_AffiliateSel"; }
        }
    }
}