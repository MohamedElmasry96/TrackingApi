namespace OrderTrackingApi.DTOs
{
    /// <summary>
    /// Represents a request to create a new tracking order with all required details.
    /// </summary>
    public class TrackingRequest
    {
        /// <summary>
        /// Gets or sets the status of the tracking order (e.g., "Pending", "Shipped").
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the unique order number for the tracking order.
        /// </summary>
        public string OrderNumber { get; set; }

        /// <summary>
        /// Gets or sets the list of tracking numbers associated with the order.
        /// </summary>
        public List<string> TrackingNumbers { get; set; }

        /// <summary>
        /// Gets or sets the name of the client who placed the order.
        /// </summary>
        public string ClientName { get; set; }

        /// <summary>
        /// Gets or sets the delivery address for the order.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets the date when the order was placed.
        /// </summary>
        public DateTime OrderDate { get; set; }

        /// <summary>
        /// Gets or sets the date when the order was shipped.
        /// </summary>
        public DateTime ShippingDate { get; set; }
    }
}