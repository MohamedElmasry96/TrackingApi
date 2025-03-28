using Microsoft.EntityFrameworkCore;
using OrderTrackingApi.Data;
using OrderTrackingApi.Interfaces;
using OrderTrackingApi.Models;

namespace TrackingApi.Services
{
    /// <summary>
    /// Service for querying tracking orders from the database.
    /// </summary>
    public class TrackingQueryService : ITrackingQueryService
    {
        private readonly TrackingDbContext _context;

        /// <summary>
        /// Initializes a new instance of the TrackingQueryService with the database context.
        /// </summary>
        /// <param name="context">The database context for tracking orders.</param>
        public TrackingQueryService(TrackingDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves all tracking orders with optional filtering by client name, start date, or end date.
        /// </summary>
        /// <param name="filter">Filter parameters (e.g., ClientName, StartDate, EndDate).</param>
        /// <returns>A list of tracking orders matching the filter.</returns>
        public async Task<List<TrackingModel>> GetAllTrackings(TrackingFilterRequest filter)
        {
            var query = _context.Orders.AsQueryable();

            // Filtering
            if (!string.IsNullOrEmpty(filter.ClientName))
                query = query.Where(o => o.ClientName == filter.ClientName);
            if (filter.StartDate.HasValue)
                query = query.Where(o => o.ShippingDate >= filter.StartDate.Value);
            if (filter.EndDate.HasValue)
                query = query.Where(o => o.ShippingDate <= filter.EndDate.Value);

            return await query.ToListAsync();
        }

        /// <summary>
        /// Retrieves a specific tracking order by its order number.
        /// </summary>
        /// <param name="orderNumber">The order number to search for.</param>
        /// <returns>The tracking order if found, otherwise null.</returns>
        public async Task<TrackingModel?> GetTracking(string orderNumber)
        {
            return await _context.Orders.FirstOrDefaultAsync(o => o.OrderNumber == orderNumber);
        }
    }
}