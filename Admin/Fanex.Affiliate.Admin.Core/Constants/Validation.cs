namespace Fanex.Affiliate.Admin.Core.Constants
{
    public static class Validation
    {
        public const int MinId = 1;
        public const int MaxId = int.MaxValue;
        public const int MinPageIndex = 1;
        public const int MaxPageIndex = int.MaxValue;
        public const int MinPageSize = 1;
        public const int MaxPageSize = 500;
        public const int MinSearchBy = 0;
        public const int MaxSearchBy = int.MaxValue;

        public const decimal InfinityAmount = 100000000000000m;
        public const int InfinityQualifiedMember = 1000000000;

        public const int MinAffType = 2;
        public const int MaxAffType = 4;

        public const int MaxLengthCommGroupDescription = 100;
        public const int MaxLengthLandingPage = 100;
    }
}
