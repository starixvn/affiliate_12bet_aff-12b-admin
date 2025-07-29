namespace Fanex.Affiliate.Link.Constants
{
    /// <summary>
    /// Provides route constraint patterns for affiliate link routing.
    /// Patterns are dynamically generated from existing constants to ensure consistency.
    /// </summary>
    public static class RouteConstraints
    {
        /// <summary>
        /// Language constraint pattern generated from Language.SupportedLanguageCodes and RemovedLanguageCodes.
        /// Includes: en, bd, bn, hien, hitg, id, jp, ko, th, vn, cs, mycs, ch, kh, eneuro
        /// </summary>
        public static readonly string LanguagePattern =
            @"(" + string.Join("|", Language.SupportedLanguageCodes.Keys
                .Concat(Language.RemovedLanguageCodes.Keys)
                .Distinct()
                .OrderByDescending(x => x.Length) // Longer patterns first to avoid partial matches
                ) + ")";

        /// <summary>
        /// Affiliate ID pattern for legacy routes - numeric only.
        /// </summary>
        public const string AffiliateIdPattern = @"\d+";

        /// <summary>
        /// Affiliate code pattern for new routes - numeric only.
        /// Same as AffiliateIdPattern since both are numeric.
        /// </summary>
        public const string AffiliateCodePattern = AffiliateIdPattern;

        /// <summary>
        /// Channel pattern allowing alphanumeric characters, hyphens, and underscores.
        /// </summary>
        public const string ChannelPattern = @"[a-zA-Z0-9_-]+";

        /// <summary>
        /// Page code pattern allowing alphanumeric characters, hyphens, and underscores.
        /// Supports both simple page codes (e.g., "home").
        /// </summary>
        public const string PageCodePattern = @"[a-zA-Z0-9_-]+";

        // Full regex patterns with anchors for GeneratedRegex attributes

        /// <summary>
        /// Complete numeric regex pattern with anchors for exact matching.
        /// Used for both affiliate IDs and affiliate codes since they have the same pattern.
        /// </summary>
        public const string NumericRegexPattern = @"^" + AffiliateIdPattern + @"$";

        /// <summary>
        /// Complete page code regex pattern with anchors for exact matching.
        /// </summary>
        public const string PageCodeRegexPattern = @"^[a-zA-Z0-9/_-]+$";

        /// <summary>
        /// Complete channel regex pattern with anchors for exact matching.
        /// </summary>
        public const string ChannelRegexPattern = @"^" + ChannelPattern + @"$";
    }
}