using BestFlight.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BestFlight.Infrastructure.Context
{
    public class BestFlightContext(DbContextOptions<BestFlightContext> options) : DbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<FlightRoutes>().HasKey(m => m.Id);
            base.OnModelCreating(builder);
        }
        public DbSet<FlightRoutes> FlightRoute { get; set; } = null!;
    }
}
