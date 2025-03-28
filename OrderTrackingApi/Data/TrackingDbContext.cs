using Microsoft.EntityFrameworkCore;
using OrderTrackingApi.Models;

namespace OrderTrackingApi.Data
{
    /// <summary>
    /// Database context for managing tracking orders using Entity Framework Core.
    /// </summary>
    public class TrackingDbContext : DbContext
    {
        /// <summary>
        /// Gets or sets the collection of tracking orders in the database.
        /// </summary>
        public DbSet<TrackingModel> Orders { get; set; }

        /// <summary>
        /// Configures the database schema and data conversions for the TrackingModel entity.
        /// </summary>
        /// <param name="modelBuilder">The model builder to configure the entity mappings.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Convert TrackingNumbers list to a comma-separated string and back
            modelBuilder.Entity<TrackingModel>()
                .Property(o => o.TrackingNumbers)
                .HasConversion(
                    trackingNumbers => string.Join(",", trackingNumbers),
                    trackingNumbersString => trackingNumbersString.Split(",", StringSplitOptions.RemoveEmptyEntries).ToList()
                );

            // Convert OrderDate to string and back for database storage
            modelBuilder.Entity<TrackingModel>()
                .Property(o => o.OrderDate)
                .HasConversion(
                    date => date.ToString("o"),
                    dateString => DateTime.Parse(dateString)
                );

            // Convert ShippingDate to string and back for database storage
            modelBuilder.Entity<TrackingModel>()
                .Property(o => o.ShippingDate)
                .HasConversion(
                    date => date.ToString("o"),
                    dateString => DateTime.Parse(dateString)
                );
        }

        /// <summary>
        /// Initializes a new instance of the TrackingDbContext with the specified options.
        /// </summary>
        /// <param name="options">The options for configuring the database context.</param>
        public TrackingDbContext(DbContextOptions<TrackingDbContext> options) : base(options)
        {
        }
    }
}