using System.ComponentModel.DataAnnotations;

namespace OrderTrackingApi.Models
{
    /// <summary>
    /// Represents a tracking order entity in the database.
    /// </summary>
    public class TrackingModel
    {
        /// <summary>
        /// Gets or sets the unique order number (primary key).
        /// </summary>
        [Key]
        public string OrderNumber { get; set; }

        /// <summary>
        /// Gets or sets the list of tracking numbers associated with the order.
        /// </summary>
        public List<string> TrackingNumbers { get; set; } = new List<string>();

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

        /// <summary>
        /// Gets or sets the status of the tracking order (e.g., "Pending", "Shipped").
        /// </summary>
        public string Status { get; set; }
    }
}