namespace Fanex.Affiliate.Link.Services.Interface
{
    using Fanex.Affiliate.Link.Commands;
    using Fanex.Affiliate.Link.DTOs;
    using System.Collections.Generic;

    /// <summary>
    /// Tracking system statistics.
    /// </summary>
    public interface ITrackingSystemService
    {
        /// <summary>
        /// Insert statistics into database.
        /// </summary>
        /// <param name="command">Input command.</param>
        /// <returns>Success or fail.</returns>
        string InsertStatistic(TrackingSystemCommand command);

        bool ToggleMarketingLink(ToggleMarketingLinkCommand command);

        CheckMarketingLinkResult GetTrackingMarketingLinkInfo(CheckMarketingLinkCommand command);

        IEnumerable<MarketInfo> GetMarkets();
    }
}
