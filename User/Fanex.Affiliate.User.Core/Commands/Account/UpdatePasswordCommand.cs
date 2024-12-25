namespace Fanex.Affiliate.User.Core.Commands.Account
{
    using Data;

    public class UpdatePasswordCommand : BaseNonQueryCommand
    {
        public int AffId { get; set; }

        public int UserId => AffId;

        [SpParam(SqlParamName = "OldPwd")]
        public string OldPassword { get; set; }

        [SpParam(SqlParamName = "AffPwd")]
        public string NewPassword { get; set; }

        public string Ip { get; set; }

        public string Language { get; set; }

        public override string GetSettingKey() => "Aff_AffiliateUpdPwd";

        public override bool IsValid()
            => AffId > 0
               && !string.IsNullOrEmpty(OldPassword)
               && !string.IsNullOrEmpty(NewPassword);
    }
}
