namespace Fanex.Affiliate.Admin.Core.Criterias.Home
{
    public class UserOnlineCriteria : PagingCriteria
    {
        private const string SP_NAME = "Aff_UserOnlineList";

        public override string GetSettingKey() => SP_NAME;

        public override bool IsValid() => PageIndex > 0 && PageSize > 0;
    }
}
