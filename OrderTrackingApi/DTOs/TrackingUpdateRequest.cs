namespace OrderTrackingApi.DTOs
{
    /// <summary>
    /// Represents a request to update an existing tracking order with optional fields.
    /// </summary>
    public class TrackingUpdateRequest
    {
        /// <summary>
        /// Gets or sets the updated status of the tracking order (e.g., "Pending", "Shipped"). Optional.
        /// </summary>
        public string? Status { get; set; }

        /// <summary>
        /// Gets or sets the updated name of the client who placed the order. Optional.
        /// </summary>
        public string? ClientName { get; set; }

        /// <summary>
        /// Gets or sets the updated delivery address for the order. Optional.
        /// </summary>
        public string? Address { get; set; }

        /// <summary>
        /// Gets or sets the updated shipping date for the order. Optional.
        /// </summary>
        public DateTime ShippingDate { get; set; }
    }
}