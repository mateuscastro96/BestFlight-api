using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace BestFlight.Infrastructure.Context
{
    public class BestFlightFactory : IDesignTimeDbContextFactory<BestFlightContext>
    {
        public BestFlightContext CreateDbContext(string[] args)
        {
            var connectionString = "Data Source=BestFlight.db";
            var optionsBuilder = new DbContextOptionsBuilder<BestFlightContext>();
            optionsBuilder.UseSqlite(connectionString);
            return new BestFlightContext(optionsBuilder.Options);
        }
    }
}
