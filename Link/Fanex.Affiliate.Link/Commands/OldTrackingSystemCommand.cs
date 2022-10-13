namespace Fanex.Affiliate.Link.Commands
{
    using Data.Repository;

    /// <summary>
    /// Tracking System Criteria.
    /// </summary>
    public class OldTrackingSystemCommand : NonQueryCommand
    {
        public int AffiliateID { get; set; }

        public string TrackingCode { get; set; }

        public string IP { get; set; }

        public string Browser { get; set; }

        public string URLReferrer { get; set; }

        public byte AffType { get; set; }

        public int ErrCode { get; set; }

        public string ErrMsg { get; set; }

        public override string GetSettingKey()
        {
            return "Aff_AffiliateInsClickStatisticsOld";
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}