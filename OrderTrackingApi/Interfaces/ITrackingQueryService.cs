using OrderTrackingApi.Models;

namespace OrderTrackingApi.Interfaces
{
    /// <summary>
    /// Defines methods for querying tracking orders from the database.
    /// </summary>
    public interface ITrackingQueryService
    {
        /// <summary>
        /// Retrieves all tracking orders with optional filtering.
        /// </summary>
        /// <param name="filter">Filter parameters (e.g., ClientName, StartDate, EndDate).</param>
        /// <returns>A list of tracking orders matching the filter.</returns>
        Task<List<TrackingModel>> GetAllTrackings(TrackingFilterRequest filter);

        /// <summary>
        /// Retrieves a specific tracking order by its order number.
        /// </summary>
        /// <param name="orderNumber">The order number to search for.</param>
        /// <returns>The tracking order if found, otherwise null.</returns>
        Task<TrackingModel> GetTracking(string orderNumber);
    }
}