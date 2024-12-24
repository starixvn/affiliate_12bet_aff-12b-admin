namespace Fanex.Affiliate.User.Core.Criterias
{
    using Utilities;

    public class GetUserInfoCriteria : BaseCriteria
    {
        private const string SpKey = "Aff_User_Login_UserInfoSel";

        public GetUserInfoCriteria(string userName, bool isInternal)
        {
            UserName = userName;
            IsInternal = isInternal;
        }

        public string UserName { get; set; }

        public override string GetSettingKey() => DbSettingHelper.BuildSpKey(IsInternal, SpKey);

        public override bool IsValid() => !string.IsNullOrWhiteSpace(UserName);
    }
}
