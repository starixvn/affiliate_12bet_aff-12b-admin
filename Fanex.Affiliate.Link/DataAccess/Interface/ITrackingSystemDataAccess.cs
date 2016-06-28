namespace Fanex.Affiliate.Link.DataAccess.Interface
{
    using Fanex.Affiliate.Link.Criterias;

    public interface ITrackingSystemDataAccess
    {
        /// <summary>
        /// Insert statistics into database.
        /// </summary>
        /// <param name="criteria">Input criteria.</param>
        /// <returns>Success or fail.</returns>
        string InsertStatistic(TrackingSystemCriteria criteria);
    }
}
