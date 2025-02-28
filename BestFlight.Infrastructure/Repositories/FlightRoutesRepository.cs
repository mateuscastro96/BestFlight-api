using BestFlight.Domain.Models;
using BestFlight.Infrastructure.Context;
using BestFlight.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BestFlight.Infrastructure.Repositories
{
    public class FlightRoutesRepository(BestFlightContext context) : IFlightRoutesRepository
    {
        public async Task<FlightRoutes?> FindAsync(Guid id)
        {
            var entity = await context.FlightRoute.FirstOrDefaultAsync(x => x.Id.Equals(id));
            return entity;
        }

        public async Task<IEnumerable<FlightRoutes?>> FindAllAsync()
        {
            var entities = await context.FlightRoute.ToListAsync();
            return entities;
        }

        public async Task<FlightRoutes> CreateAsync(FlightRoutes entity)
        {
            await context.FlightRoute.AddAsync(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task<FlightRoutes> Update(FlightRoutes entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            context.FlightRoute.Update(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(FlightRoutes entity)
        {
            context.FlightRoute.Remove(entity);
            await context.SaveChangesAsync();
        }
    }
}
