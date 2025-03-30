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
        public async Task<string> AddTracking(TrackingRequest trackingRequest)
        {
            // Get the last OrderNumber from the database
            var lastOrder = await _context.Orders
                .OrderByDescending(t => t.OrderNumber)
                .FirstOrDefaultAsync();

            // Generate the new OrderNumber (e.g., ORD001, ORD002, etc.)
            string newOrderNumber = "ORD001"; // Default if no orders exist
            if (lastOrder != null)
            {
                // Extract the number part (e.g., "001" from "ORD001")
                string lastNumberStr = lastOrder.OrderNumber.Substring(3); // Skip "ORD"
                if (int.TryParse(lastNumberStr, out int lastNumber))
                {
                    // Increment the number and format it as ORD + 3 digits (e.g., ORD002)
                    newOrderNumber = $"ORD{(lastNumber + 1):D3}";
                }
            }

            TrackingModel model = new TrackingModel
            {
                // Generate OrderNumber dynamically
                OrderNumber = newOrderNumber,
                // Generate TrackingNumbers dynamically (e.g., one tracking number for now)
                TrackingNumbers = new List<string> { "TRK-" + Guid.NewGuid().ToString().Substring(0, 5) },
                // Set OrderDate dynamically
                OrderDate = DateTime.UtcNow,
                // Take these from the request (user input)
                ClientName = trackingRequest.ClientName,
                Address = trackingRequest.Address,
                ShippingDate = trackingRequest.ShippingDate,
                Status = trackingRequest.Status
            };

            _context.Orders.Add(model);
            await _context.SaveChangesAsync();

            return model.OrderNumber;
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

                if (!string.IsNullOrEmpty(trackingUpdate.Status))
                    order.Status = trackingUpdate.Status;

                order.ShippingDate = trackingUpdate.ShippingDate;

                await _context.SaveChangesAsync();
            }
        }
    }
}