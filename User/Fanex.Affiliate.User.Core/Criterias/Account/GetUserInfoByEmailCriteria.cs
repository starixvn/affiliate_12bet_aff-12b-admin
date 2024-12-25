namespace Fanex.Affiliate.User.Core.Criterias
{
    using Data.Repository;

    public class GetUserInfoByEmailCriteria : CriteriaBase
    {
        public string Email { get; set; }

        public string ValidCode { get; set; }

        public string Ip { get; set; }

        public int ErrCode { get; set; }

        public string Language { get; set; }

        public override string GetSettingKey() => "Aff_User_ForgotPassword";

        public override bool IsValid() => !string.IsNullOrEmpty(Email);
    }
}
