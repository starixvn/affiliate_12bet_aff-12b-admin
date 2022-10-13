namespace Fanex.Affiliate.Link.Services.Interface
{
    using Fanex.Affiliate.Link.Commands;

    /// <summary>
    /// Tracking system statistics.
    /// </summary>
    public interface ITrackingSystemService
    {
        /// <summary>
        /// Insert statistics into database.
        /// </summary>
        /// <param name="criteria">Input criteria.</param>
        /// <returns>Success or fail.</returns>
        string InsertStatistic(TrackingSystemCommand command);

        string InsertOldStatistic(OldTrackingSystemCommand command);
    }
}
