namespace Fanex.Affiliate.User.Core.Criterias
{
    using Data.Repository;

    public class GetSecurityQuestionsCriteria : CriteriaBase
    {
        public GetSecurityQuestionsCriteria(string language)
        {
            Language = language;
        }

        public string Language { get; set; }

        public override string GetSettingKey() => "Aff_User_SecurityQuestions_Sel";

        public override bool IsValid() => !string.IsNullOrWhiteSpace(Language);
    }
}
