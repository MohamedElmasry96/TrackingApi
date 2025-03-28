using Microsoft.AspNetCore.Mvc;
using OrderTrackingApi.DTOs;
using OrderTrackingApi.Interfaces;
using OrderTrackingApi.Models;

namespace OrderTrackingApi.Controllers
{
    /// <summary>
    /// Controller for managing tracking orders via RESTful API endpoints.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class TrackingController : ControllerBase
    {
        private readonly ITrackingQueryService _queryService;
        private readonly ITrackingCommandService _commandService;

        /// <summary>
        /// Initializes a new instance of the TrackingController with dependency injection.
        /// </summary>
        /// <param name="queryService">Service for querying tracking data.</param>
        /// <param name="commandService">Service for modifying tracking data.</param>
        public TrackingController(ITrackingQueryService queryService, ITrackingCommandService commandService)
        {
            _queryService = queryService;
            _commandService = commandService;
        }

        /// <summary>
        /// Retrieves all tracking orders with optional filtering.
        /// </summary>
        /// <param name="filter">Filter parameters (e.g., ClientName, StartDate, EndDate).</param>
        /// <returns>A list of tracking orders.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllTrackings([FromQuery] TrackingFilterRequest filter)
        {
            var orders = await _queryService.GetAllTrackings(filter);
            return Ok(orders);
        }

        /// <summary>
        /// Retrieves a specific tracking order by its order number.
        /// </summary>
        /// <param name="orderNumber">The order number to search for.</param>
        /// <returns>The tracking order if found, otherwise 404 Not Found.</returns>
        [HttpGet("{orderNumber}")]
        public async Task<IActionResult> GetTracking(string orderNumber)
        {
            var order = await _queryService.GetTracking(orderNumber);
            if (order == null) return NotFound();
            else return Ok(order);
        }

        /// <summary>
        /// Creates a new tracking order.
        /// </summary>
        /// <param name="trackingRequest">The tracking order details to create.</param>
        /// <returns>201 Created with the new order details, or 500 if an error occurs.</returns>
        [HttpPost]
        public async Task<IActionResult> PostTracking(TrackingRequest trackingRequest)
        {
            try
            {
                await _commandService.AddTracking(trackingRequest);
                return CreatedAtAction(nameof(GetTracking), new { orderNumber = trackingRequest.OrderNumber }, trackingRequest);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        /// <summary>
        /// Updates an existing tracking order by its order number.
        /// </summary>
        /// <param name="orderNumber">The order number of the tracking order to update.</param>
        /// <param name="trackingUpdate">The updated tracking order details.</param>
        /// <returns>204 No Content if successful, 404 if not found, or 500 if an error occurs.</returns>
        [HttpPut("{orderNumber}")]
        public async Task<IActionResult> UpdateTracking(string orderNumber, [FromBody] TrackingUpdateRequest trackingUpdate)
        {
            try
            {
                var order = await _queryService.GetTracking(orderNumber);
                if (order == null) return NotFound();
                await _commandService.UpdateTracking(orderNumber, trackingUpdate);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        /// <summary>
        /// Deletes a tracking order by its order number.
        /// </summary>
        /// <param name="orderNumber">The order number of the tracking order to delete.</param>
        /// <returns>204 No Content if successful, or 404 if not found.</returns>
        [HttpDelete("{orderNumber}")]
        public async Task<IActionResult> DeleteTracking(string orderNumber)
        {
            var order = await _queryService.GetTracking(orderNumber);
            if (order == null) return NotFound();
            await _commandService.DeleteTracking(orderNumber);
            return NoContent();
        }
    }
}