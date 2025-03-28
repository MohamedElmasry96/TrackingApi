using Microsoft.EntityFrameworkCore;
using OrderTrackingApi.Data;
using OrderTrackingApi.DTOs;
using OrderTrackingApi.Interfaces;
using OrderTrackingApi.Models;

namespace OrderTrackingApi.Services
{
    /// <summary>
    /// Service for handling commands to modify tracking orders (create, update, delete).
    /// </summary>
    public class TrackingCommandService : ITrackingCommandService
    {
        private readonly TrackingDbContext _context;

        /// <summary>
        /// Initializes a new instance of the TrackingCommandService with the database context.
        /// </summary>
        /// <param name="trackingDb">The database context for tracking orders.</param>
        public TrackingCommandService(TrackingDbContext trackingDb)
        {
            _context = trackingDb;
        }

        /// <summary>
        /// Adds a new tracking order to the database.
        /// </summary>
        /// <param name="trackingRequest">The tracking order details to add.</param>
        public async Task AddTracking(TrackingRequest trackingRequest)
        {
            TrackingModel model = new TrackingModel
            {
                OrderNumber = trackingRequest.OrderNumber,
                ClientName = trackingRequest.ClientName,
                TrackingNumbers = trackingRequest.TrackingNumbers,
                Address = trackingRequest.Address,
                OrderDate = trackingRequest.OrderDate,
                ShippingDate = trackingRequest.ShippingDate,
                Status = trackingRequest.Status
            };
            _context.Orders.Add(model);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Deletes a tracking order by its order number.
        /// </summary>
        /// <param name="orderNumber">The order number of the tracking order to delete.</param>
        public async Task DeleteTracking(string orderNumber)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.OrderNumber == orderNumber);
            if (order != null)
            {
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Updates an existing tracking order by its order number with optional fields.
        /// </summary>
        /// <param name="orderNumber">The order number of the tracking order to update.</param>
        /// <param name="trackingUpdate">The updated tracking order details.</param>
        public async Task UpdateTracking(string orderNumber, TrackingUpdateRequest trackingUpdate)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.OrderNumber == orderNumber);

            if (order != null)
            {
                if (!string.IsNullOrEmpty(trackingUpdate.ClientName))
                    order.ClientName = trackingUpdate.ClientName;

                if (!string.IsNullOrEmpty(trackingUpdate.Address))
                    order.Address = trackingUpdate.Address;

                if (trackingUpdate.ShippingDate.HasValue)
                    order.ShippingDate = trackingUpdate.ShippingDate.Value;

                if (!string.IsNullOrEmpty(trackingUpdate.Status))
                    order.Status = trackingUpdate.Status;

                await _context.SaveChangesAsync();
            }
        }
    }
}