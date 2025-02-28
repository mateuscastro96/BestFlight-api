using BestFlight.Domain.Models;

namespace BestFlight.Infrastructure.Repositories.Interfaces
{
    public interface IFlightRoutesRepository
    {
        Task<FlightRoutes?> FindAsync(Guid id);
        Task<IEnumerable<FlightRoutes?>> FindAllAsync();
        Task<FlightRoutes> CreateAsync(FlightRoutes entity);
        Task<FlightRoutes> Update(FlightRoutes entity);
        Task DeleteAsync(FlightRoutes entity);
    }
}
