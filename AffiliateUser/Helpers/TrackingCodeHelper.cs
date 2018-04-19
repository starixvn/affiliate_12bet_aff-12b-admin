namespace AffiliateUser.Helpers
{
    public static class TrackingCodeHelper
    {
        public static string Convert(string code)
        {
            return code.Equals("cricket", System.StringComparison.OrdinalIgnoreCase) ? "indiancricket" : code;
        }
    }
}