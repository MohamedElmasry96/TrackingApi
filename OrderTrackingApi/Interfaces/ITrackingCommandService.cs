using OrderTrackingApi.DTOs;

namespace OrderTrackingApi.Interfaces
{
    /// <summary>
    /// Defines methods for modifying tracking orders (create, update, delete).
    /// </summary>
    public interface ITrackingCommandService
    {
        /// <summary>
        /// Adds a new tracking order to the database.
        /// </summary>
        /// <param name="trackingRequest">The tracking order details to add.</param>
        Task AddTracking(TrackingRequest trackingRequest);

        /// <summary>
        /// Updates an existing tracking order by its order number.
        /// </summary>
        /// <param name="orderNumber">The order number of the tracking order to update.</param>
        /// <param name="trackingUpdate">The updated tracking order details.</param>
        Task UpdateTracking(string orderNumber, TrackingUpdateRequest trackingUpdate);

        /// <summary>
        /// Deletes a tracking order by its order number.
        /// </summary>
        /// <param name="orderNumber">The order number of the tracking order to delete.</param>
        Task DeleteTracking(string orderNumber);
    }
}