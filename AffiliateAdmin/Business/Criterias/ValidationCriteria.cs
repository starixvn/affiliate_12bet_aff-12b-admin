namespace Criterias
{
    public class ValidationCriteria
    {
        public string AdminName { get; set; }

        public int AffId { get; set; }

        public string MemberName { get; set; }

        public byte BelongLevel1 { get; set; }

        public byte BelongLevel2 { get; set; }

        public string SpName
        {
            get
            {
                return "Aff_BetList_CheckBelonging";
            }
        }
    }
}