namespace Fanex.Affiliate.User.Core.Commands.Account
{
    public class CreateSecretKeyCommand : BaseNonQueryCommand
    {
        public CreateSecretKeyCommand(int affId, string secretKey)
        {
            AffId = affId;
            SecretKey = secretKey;
        }

        public int AffId { get; set; }

        public string SecretKey { get; set; }

        public override string GetSettingKey() => "Aff_User_SecretKeyIns";

        public override bool IsValid() => AffId > 0 && !string.IsNullOrEmpty(SecretKey);
    }
}
