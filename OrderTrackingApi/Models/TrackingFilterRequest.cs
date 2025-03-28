namespace OrderTrackingApi.Models
{
    /// <summary>
    /// Represents a filter request for querying tracking orders with optional criteria.
    /// </summary>
    public class TrackingFilterRequest
    {
        /// <summary>
        /// Gets or sets the client name to filter orders by. Optional.
        /// </summary>
        public string? ClientName { get; set; }

        /// <summary>
        /// Gets or sets the start date to filter orders from. Optional.
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Gets or sets the end date to filter orders until. Optional.
        /// </summary>
        public DateTime? EndDate { get; set; }
    }
}