namespace Fanex.Affiliate.User.Core.Commands
{
    public class UpdateLoginResultCommand : BaseNonQueryCommand
    {
        public int AffId { get; set; }

        public string IpAddress { get; set; }

        public string DeviceInfo { get; set; }

        public string Language { get; set; }

        public bool IsSuccessful { get; set; }

        public override string GetSettingKey() => "Aff_User_Login_ResultUpd";

        public override bool IsValid() => AffId > 0;
    }
}
