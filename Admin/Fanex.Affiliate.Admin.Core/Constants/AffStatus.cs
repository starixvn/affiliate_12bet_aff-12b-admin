using Fanex.Affiliate.Admin.Core.Extensions;

namespace Fanex.Affiliate.Admin.Core.Constants
{
    public static class AffStatus
    {
        public const string Approved = "approved";
        public const string Disapproved = "disapproved";
        public const string Pending = "pending";
        public const string Rejected = "rejected";

        public static bool IsPendingAff(string status) => Pending.MatchWith(status);
    }
}