namespace Fanex.Affiliate.User.Core.Criterias
{
    using Data.Repository;

    public class GetLoginInfoByUserDeviceCriteria : CriteriaBase
    {
        public int AffId { get; set; }

        public string IpAddress { get; set; }

        public string DeviceInfo { get; set; }

        public override string GetSettingKey() => "Aff_User_VerificationInfoSel";

        public override bool IsValid() => AffId > 0;
    }
}
