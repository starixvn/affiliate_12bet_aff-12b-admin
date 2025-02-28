namespace Fanex.Affiliate.User.Core.Criterias
{
    using Data.Repository;

    public class GetRecoverPasswordCriteria : CriteriaBase
    {
        public int AffId { get; set; }

        public string ValidCode { get; set; }

        public string NewPassword { get; set; }

        public int ErrCode { get; set; }

        public string Ip { get; set; }

        public string Language { get; set; }

        public override string GetSettingKey() => "Aff_User_RecoverPassWord";

        public override bool IsValid()
            => AffId > 0
               && !string.IsNullOrEmpty(ValidCode)
               && !string.IsNullOrEmpty(NewPassword);
    }
}
