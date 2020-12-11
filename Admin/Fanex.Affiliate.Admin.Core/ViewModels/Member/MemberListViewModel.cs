namespace Fanex.Affiliate.Admin.Core.ViewModels.Member
{
    public class MemberListViewModel
    {
        public string AffName { get; set; }

        public bool IsFromOtherPages => !string.IsNullOrEmpty(AffName);
    }
}
